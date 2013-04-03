namespace Senquevila
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// Clase que controla Procesos
  /// </summary>
  public class Proceso
  {
    /// <summary>
    /// Campo de proceso
    /// </summary>
    private Process proc;

    /// <summary>
    /// Campo de ProcessStartInfo
    /// </summary>
    private ProcessStartInfo psi;

    /// <summary>
    /// Initializes a new instance of the Proceso class
    /// </summary>
    public Proceso()
    {
    }

    /// <summary>
    /// Funcion que sirve para ejecutar un proceso
    /// </summary>
    /// <param name="ruta">Direccion del proceso</param>
    /// <param name="pws">Estilo de Ventana</param>
    /// <param name="usingProcessStartInfo">Utiliza ProcessStartInfo</param>
    public void Run(string rutaStr, ProcessWindowStyle pws, bool usingProcessStartInfo = false)
    {
      if (rutaStr == null || rutaStr.Equals(string.Empty)) {
        throw new ArgumentNullException("ruta");
      }

      if (usingProcessStartInfo) {
        this.psi = new ProcessStartInfo(rutaStr);
        this.psi.WindowStyle = pws;
        this.proc = new Process();
        this.proc = Process.Start(this.psi);
      }
      else {
        this.proc = Process.Start(rutaStr);
        this.proc.StartInfo.WindowStyle = pws;
      }

      this.proc.WaitForExit();
    }
    
    public void Run(string rutaStr, string argumento, bool esperar = false, bool useShell = false)
    {
      if (rutaStr == null || rutaStr.Equals(string.Empty)) {
        throw new ArgumentNullException("ruta");
      }

      if (argumento == null) {
        throw new ArgumentNullException("psw");
      }

      this.proc = new Process();
      this.proc.StartInfo.FileName = rutaStr;
      this.proc.StartInfo.Arguments = argumento;
      this.proc.StartInfo.UseShellExecute = useShell;
      
      try {
        this.proc.Start();
        
        if (esperar) {
          this.proc.WaitForExit();
        }
      }
      catch (Exception e) {
        Console.WriteLine(e.StackTrace);
      }
    }
  }
}