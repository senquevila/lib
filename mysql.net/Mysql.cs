// Access.cs
//

namespace Reloj
{
    using System;
    using System.Data;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Security.Cryptography;
    using MySql.Data.MySqlClient;
    using System.Windows.Forms;

    /// <summary>
    /// Clase que maneja la conexion a la Base de Datos MySQL
    /// </summary>
    public class Mysql {

        /// <summary>
        /// Creates a new instance of Access
        /// </summary>
        private static MySqlConnection conn;
		private static MySqlDataAdapter da;
		private static MySqlCommand cmd;
		private static DataSet ds;
		private static DataTable dt;

		//.....................................................................................

		public Mysql(){
            conn = new MySqlConnection("Database=reloj;Data Source=129.200.10.11;User Id=reloj;Password=reloj123");
            da = new MySqlDataAdapter();
		}

		//.....................................................................................

		private bool conexion(){
			try{
				conn.Open();
				return true;
			} catch(Exception){
				return false;
			}
		}

		//.....................................................................................

		public void cerrar(){
			try{
				conn.Close();
			} catch(Exception){}
		}

		//.....................................................................................

		public string DbName(){
			return conn.Database;
		}

		//.....................................................................................

		public DataTable Select(string sql){
			if(conexion()){
				ds = new DataSet();
				dt = new DataTable();
				cmd = new MySqlCommand(sql, conn);
                				
				da.SelectCommand = cmd;

				try{
					cmd.ExecuteNonQuery();
					da.Fill(ds);
					dt = ds.Tables[0];
					return dt;
				} catch(Exception){}
				finally{
					cerrar();
				}
			}
			return null;
		}

        //.....................................................................................
        
       public string[,] printTable(DataTable tabla) {
        	string[,] salida;
			try{
        		if(tabla.Rows.Count != 0) {
					salida = new string[tabla.Rows.Count + 1, tabla.Columns.Count];
		        	for(int k=0; k<tabla.Columns.Count; k++) {
		        		salida[0,k] = tabla.Columns[k].ToString();
		        	}
		        	int i = 1;
		        	foreach(DataRow dr in tabla.Rows) {
		        		for(int j=0; j<tabla.Columns.Count; j++) {
		        			salida[i,j] = dr[j].ToString();
		        		}
		        		i++;
		        	}
        		} else{
        			salida = new string[1,1];
        			salida[0,0] = "SIN DATOS";        		
        		}
        	} catch {
        		salida = new string[1,1];
        		salida[0,0] = "SIN DATOS";        		
        	}
        	return salida;
        }
        
        //.....................................................................................
        
        public Boolean Query(string sql)
        {
            conexion();
            cmd = new MySqlCommand(sql, conn);
            da.InsertCommand = cmd;
            try{
                cmd.ExecuteNonQuery();
                cerrar();
                return true;
            } catch (Exception e) {
            	MessageBox.Show(e.Message);
                cerrar();
                return false;
            }            
        }

        //.....................................................................................
        
        public MySqlDataAdapter getAdapter(){
        	return da;
        }
        
        //.....................................................................................
        
        public MySqlConnection getConnection() {
        	return conn;
        }
        
        //.....................................................................................

        public string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
