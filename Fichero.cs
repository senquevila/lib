/*
 * Creado por SharpDevelop.
 * Usuario: Enrique
 * Fecha: 14/12/2008
 * Hora: 02:10 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */

namespace Senque
{
    using System;
    using System.Collections;
    using System.IO;
    
    /// <summary>
    /// Clase para el manejo de los contenidos en los ficheros
    /// </summary>
    public class Fichero
    {
        /// <summary>
        /// Codificacion en la que se leerá el archivo
        /// </summary>
        private static System.Text.Encoding encode;

        /// <summary>
        /// Stram Writer
        /// </summary>
        private StreamWriter sw;

        /// <summary>
        /// Stream Reader
        /// </summary>
        private StreamReader sr;

        /// <summary>
        /// Direccion del archivo
        /// </summary>
        private string path;
        
        /// <summary>
        /// Initializes a new instance of the Fichero class
        /// </summary>
        /// <param name="path">Direccion del archivo</param>
        public Fichero(string path)
        {
            this.SetPath(path);
            encode = System.Text.Encoding.Default;
        }
        
        /// <summary>
        /// Obtiene la direccion del archivo
        /// </summary>
        /// <returns>Retorna un String</returns>
        public string GetPath()
        {
            return this.path;
        }

        /// <summary>
        /// Establece la dirección del archivo
        /// </summary>
        /// <param name="path">Dirección del archivo</param>
        public void SetPath(string path)
        {
            if (path != null)
            {
                if (path != string.Empty)
                {
                    this.path = path;
                }
                else
                {
                    throw new ArgumentException("path invalido");
                }
            }
            else
            {
                throw new ArgumentNullException("path");
            }
        }
        
        /// <summary>
        /// Escribe por completo en el archivo
        /// </summary>
        /// <param name="texto">Texto que se escribirá</param>
        public void EscribirTodo(string texto)
        {
            try
            {
                this.sw = new StreamWriter(this.path);
                this.sw.Write(texto);
            }
            finally
            {
                this.sw.Close();
            }
        }
        
        /// <summary>
        /// Escribe en el archivo por lineas, sin crear un salto de linea
        /// </summary>
        /// <param name="linea">Número de linea que se escribirá en el archivo</param>
        public void EscribirLinea(string linea)
        {
            try
            {
                this.sw = new StreamWriter(this.path, true);
                this.sw.Write(linea);
            }
            catch
            {
            }
            finally
            {
                this.sw.Close();
            }
        }
        
        /// <summary>
        /// Escribe en el archivo por lineas, creando un salto de linea
        /// </summary>
        /// <param name="linea">Numero de la linea que se escribira en el archivo</param>
        public void EscribirPorLinea(string linea)
        {
            try
            {
                this.sw = new StreamWriter(this.path, true);
                this.sw.WriteLine(linea);
            }
            finally
            {
                this.sw.Close();
            }
        }

        /// <summary>
        /// Lee el archivo por linea y lo retorna en un arreglo
        /// </summary>
        /// <param name="linea">Numero de linea que se desea extraer del archivo</param>
        /// <returns>retorna un ArrayList</returns>
        public ArrayList LeerPorLineaArray(int linea)
        {
            ArrayList al = new ArrayList();
            try
            {
                this.sr = new StreamReader(this.path, encode);
                int i = 0;
                while (!this.sr.EndOfStream)
                {
                    i++;
                    string aux = this.sr.ReadLine();
                    if (i == linea)
                    {
                        al.Add(aux);
                    }
                }
            }
            finally
            {
                this.sr.Close();
            }

            return al;
        }
        
        /// <summary>
        /// Lee el archivo por linea y lo retorna en un string
        /// </summary>
        /// <param name="linea">Numero de linea que se desea extraer del archivo</param>
        /// <returns>retorna un String</returns>
        public String LeerPorLineaString(int linea)
        {
            string aux = string.Empty;
            try
            {
                this.sr = new StreamReader(this.path, encode);
                int i = 0;
                while (!this.sr.EndOfStream)
                {
                    i++;
                    aux = this.sr.ReadLine();
                    if (i == linea)
                    {
                        return aux;
                    }
                }
            }
            finally
            {
                this.sr.Close();
            }

            return aux;
        }

        /// <summary>
        /// Lee todo el archivo y lo retorna en un String
        /// </summary>
        /// <returns>retorna un String</returns>
        public string LeerTodoString()
        {
            string salida;
            try
            {
                this.sr = new StreamReader(this.path, encode);
                salida = this.sr.ReadToEnd();
            }
            finally
            {
                this.sr.Close();
            }

            return salida;
        }
        
        /// <summary>
        /// Lee todo el archivo y lo retorna en un arreglo
        /// </summary>
        /// <returns>retorna un ArrayList</returns>
        public ArrayList LeerTodoArray()
        {
            ArrayList al = new ArrayList();
            try
            {
                this.sr = new StreamReader(this.path, encode);
                while (!this.sr.EndOfStream)
                {
                    al.Add(this.sr.ReadLine());
                }
            }
            finally
            {
                this.sr.Close();
            }

            return al;
        }
    }
}
