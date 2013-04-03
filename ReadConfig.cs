namespace SENQUE
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// Clase para leer archivos de configuración especiales
    /// </summary>
    public class ReadConfig
    {
        /// <summary>
        /// Colección de parametros
        /// </summary>
        private Hashtable parametros;        
        /// <summary>
        /// Ruta del archivo de configuracion
        /// </summary>
        private string source;

        /// <summary>
        /// Constructor de la clase ReadConfig
        /// </summary>
        /// <param name="source">Ruta del archivo de configuración</param>
        public ReadConfig(string source)
        {
            this.source = source;
            this.SetParametros();
        }

        /// <summary>
        /// Funcion que almacena los parametros obtenidos del archivo de configuración
        /// </summary>
        private void SetParametros()
        {
            parametros = new Hashtable();
            if (File.Exists(this.source))
            {
                Fichero filex = new Fichero(this.source);
                ArrayList aux = filex.LeerTodoArray();

                if (aux != null)
                {
                    foreach (string str in aux)
                    {
                        if (!str.StartsWith("#") && !str.Equals(String.Empty))
                        {
                            try
                            {
                                string[] piezas = str.Split('=');
                                parametros.Add(piezas[0], piezas[1]);
                            }
                            finally { }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Función que muestra el parametro a elegir
        /// </summary>
        /// <param name="key">Indice que llama al valor</param>
        /// <returns>retorna un String</returns>
        public string GetParametros(object key)
        {
            try
            {
                return parametros[key].ToString();
            }
            catch { return null; }
        }

        /// <summary>
        /// Funcion que devuelve la colección de los parametros
        /// </summary>
        /// <returns>retorna un Hashtable</returns>
        public Hashtable GetParametros()
        {
            return parametros;
        }

        /// <summary>
        /// Función que devuelve una colección generada de un parametro en particular
        /// </summary>
        /// <param name="key">Indice que llama al valor</param>
        /// <returns>retorna un ArrayList</returns>
        public ArrayList GetParamArray(object key)
        {
            ArrayList al = new ArrayList();
            try
            {
                al.AddRange(this.GetParametros(key).Split(';'));
            }
            catch
            {
                al = null;
            }
            return al;
        }

        /// <summary>
        /// Función que devuelve una colección generada de un parametro en particular
        /// </summary>
        /// <param name="key">Indice que llama al valor</param>
        /// <param name="patron">Caracter que construye la coleccion</param>
        /// <returns>retorna un ArrayList</returns>
        public ArrayList GetParamArray(object key, char patron)
        {
            ArrayList al = new ArrayList();
            try
            {
                al.AddRange(this.GetParametros(key).Split(patron));
            }
            catch
            {
                al = null;
            }
            return al;
        }
    }
}
