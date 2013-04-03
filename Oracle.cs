/*
 * Created by SharpDevelop.
 * User: jeavila
 * Date: 03/03/2009
 * Time: 11:51 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Data;
using System.Data.OleDb;

namespace Senque
{
	/// <summary>
	/// Clase para usar en las conexiones con Oracle
	/// </summary>
	public class Oracle
	{
		private OleDbConnection conn;
		private OleDbDataAdapter oda;
		//private OleDbCommand ocmd;
		private DataSet ds;
		private DataTable dt;
		
		public Oracle(string connectionString)
		{
			conn = new OleDbConnection(connectionString);			
		}
		
		public Oracle(string ds, string user, string psw){
			string connectionString = "Provider=MSDAORA;Data Source=" + ds + ";Persist Security Info=True;Password=" + psw + ";User ID=" + user;
			conn = new OleDbConnection(connectionString);
		}
		
		public bool Conectar(){
			try{
				conn.Open();
				return true;
			} catch(Exception){
				return false;
			}			
		}
		
		public bool Desconectar(){
			try{
				conn.Close();
				return true;
			} catch(Exception){
				return false;
			}
		}
		
		public DataTable Resultado(string script, string tabla){
			string sql = "";
			if(script == null) sql = "SELECT * FROM " + tabla;
			else sql = script;
            if (Conectar())
            {
                this.oda = new OleDbDataAdapter(sql, this.conn);
                ds = new DataSet();
                oda.FillSchema(ds, SchemaType.Source, tabla);
                oda.Fill(ds, tabla);
                dt = ds.Tables[tabla];
                Desconectar();
                return dt;
            }
            else { return null; }
		}		
	}
}
