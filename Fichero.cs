/*
 * Creado por SharpDevelop.
 * Usuario: Enrique
 * Fecha: 14/12/2008
 * Hora: 02:10 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */

namespace Senquevila
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
    private string pathStr;
    
    /// <summary>
    /// Initializes a new instance of the Fichero class
    /// </summary>
    /// <param name="pathStr">Direccion del archivo</param>
    public Fichero(string pathStr)
    {
      this.SetPath(pathStr);
      encode = System.Text.Encoding.Default;
    }
    
    /// <summary>
    /// Obtiene la direccion del archivo
    /// </summary>
    /// <returns>Retorna un String</returns>
    public string GetPath()
    {
      return this.pathStr;
    }
    
    /// <summary>
    /// Establece la dirección del archivo
    /// </summary>
    /// <param name="pathStr">Dirección del archivo</param>
    public void SetPath(string pathStr)
    {
      if (pathStr != null) {
        if (pathStr != string.Empty) {
          this.pathStr = pathStr;
        }
        else {
          throw new ArgumentException("path invalido");
        }
      }
      else {
        throw new ArgumentNullException("path");
      }
    }
    
    /// <summary>
    /// Escribe por completo en el archivo, borra cualquier contenido anterior. 
    /// Crea el archivo si no existe.
    /// </summary>
    /// <param name="textoStr">Texto que se escribirá</param>
    public void Escribir(string textoStr)
    {
      try {
        this.sw = new StreamWriter(this.pathStr);
        this.sw.Write(textoStr);
      }
      finally {
        this.sw.Close();
      }
    }
    
    /// <summary>
    /// Escribe en el archivo por lineas, sin crear un salto de linea.
    /// Crea el archivo si no existe.
    /// </summary>
    /// <param name="lineaStr">Número de linea que se escribirá en el archivo</param>
    public void EscribirLinea(string lineaStr)
    {
      try {
        this.sw = new StreamWriter(this.pathStr, true);
        this.sw.Write(lineaStr);
      }
      finally {
        this.sw.Close();
      }
    }
       
    /// <summary>
    /// Lee el archivo por linea y lo retorna en un string
    /// </summary>
    /// <param name="linea">Numero de linea que se desea leer del archivo</param>
    /// <returns>retorna un String</returns>
    public String LineaToString(int linea)
    {
      string salidaStr = string.Empty;
      
      try {
        int i = 0;
        this.sr = new StreamReader(this.pathStr, encode);
        
        while (!this.sr.EndOfStream) {
          salidaStr = this.sr.ReadLine();
          i++;
          
          if (i == linea) {
            return salidaStr;
          }
        }
      }
      finally {
        this.sr.Close();
      }

      return salidaStr;
    }

    /// <summary>
    /// Lee todo el archivo y lo retorna en un String
    /// </summary>
    /// <returns>retorna un String</returns>
    public override string ToString()
    {
      string salidaStr = string.Empty;
      
      try
      {
        this.sr = new StreamReader(this.pathStr, encode);
        salidaStr = this.sr.ReadToEnd();
      }
      finally
      {
        this.sr.Close();
      }

      return salidaStr;
    }
    
    /// <summary>
    /// Lee todo el archivo y lo retorna en un arreglo
    /// </summary>
    /// <returns>retorna un ArrayList</returns>
    public ArrayList ToArray()
    {
      ArrayList listaAl = new ArrayList();
      
      try {
        this.sr = new StreamReader(this.pathStr, encode);
        
        while (!this.sr.EndOfStream) {
          listaAl.Add(this.sr.ReadLine());
        }
      }
      finally {
        this.sr.Close();
      }

      return listaAl;
    }
  }
}
