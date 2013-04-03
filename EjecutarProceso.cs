namespace Senque
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Clase que controla Procesos
    /// </summary>
    public class EjecutarProceso
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
        /// Initializes a new instance of the EjecutarProceso class
        /// </summary>
        public EjecutarProceso()
        {
        }

        /// <summary>
        /// Funcion que sirve para ejecutar un proceso
        /// </summary>
        /// <param name="ruta">Direccion del proceso</param>
        /// <param name="pws">Estilo de Ventana</param>
        /// <param name="usingProcessStartInfo">Utiliza ProcessStartInfo</param>
        public void Run(string ruta, ProcessWindowStyle pws, bool usingProcessStartInfo)
        {
            if (ruta == null)
            {
                throw new ArgumentNullException("ruta");
            }

            if (ruta.Equals(string.Empty) || !System.IO.File.Exists(ruta))
            {
                throw new ArgumentException("ruta invalida");
            }

            if (usingProcessStartInfo)
            {
                this.psi = new ProcessStartInfo(ruta);
                this.psi.WindowStyle = pws;
                this.proc = new Process();
                this.proc = Process.Start(this.psi);
            }
            else
            {
                this.proc = Process.Start(ruta);
                this.proc.StartInfo.WindowStyle = pws;
            }

            this.proc.WaitForExit();
        }
        
        /// <summary>
        /// Metodo que sirve para ejecutar un proceso
        /// </summary>
        /// <param name="ruta">Dirección del proceso</param>
        /// <param name="argumento">Argumentos para aplicar el proceso</param>
        /// <param name="esperar">Esperará hasta terminar el proceso</param>
        /// <param name="useShell">Utilizará el Shell para ejecutar el proceso</param>
        public void Run(string ruta, string argumento, bool esperar, bool useShell)
        {
            if (ruta == null )
            {
                throw new ArgumentNullException("ruta");
            }

            if (ruta.Equals(string.Empty) || !System.IO.File.Exists(ruta))
            {
                throw new ArgumentException("ruta invalida");
            }

            if (argumento == null)
            {
                throw new ArgumentNullException("password");
            }

            this.proc = new Process();
            this.proc.StartInfo.FileName = ruta;
            this.proc.StartInfo.Arguments = argumento;
            this.proc.StartInfo.UseShellExecute = useShell;
            try
            {
                this.proc.Start();
                if (esperar)
                {
                    this.proc.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}