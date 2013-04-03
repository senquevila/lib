/*
 * Creado por SharpDevelop.
 * Usuario: jeavila
 * Fecha: 23/03/2010
 * Hora: 09:08 a.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections;
using Tamir.SharpSsh.jsch;

namespace Senque
{
	/// <summary>
	/// Description of Sftp.
	/// </summary>
    /// 
	public class Sftp
    {
        #region Propiedades
        private Session session;
		private Channel canal;
		private ChannelSftp canalsftp;
		private string host;
		private string user;
		private string psw;
		private int port;
		private int timeOut;
        private MyProgressMonitor monitor;
        #endregion

        #region Constructor
        public Sftp(string host, string user, string password, int port, int timeOut)
		{
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if ((object)port == null)
            {
                throw new ArgumentNullException("port");
            }

            if ((object)timeOut == null)
            {
                throw new ArgumentNullException("timeOut");
            }

			this.host = host;
			this.user = user;
			this.psw = password;
			this.port = port;
			this.timeOut = timeOut;
		}
        #endregion

        #region Metodos
        private void Conectar(){
			JSch jsch = new JSch();
			Hashtable propiedades = new Hashtable();
						
			this.session=jsch.getSession(this.user, this.host, this.port);
            this.session.setPassword(this.psw);
            this.session.setTimeout(this.timeOut);
			
			propiedades.Add("password", this.psw);
			propiedades.Add("StrictHostKeyChecking", "no");

            this.session.setConfig(propiedades);
            this.session.connect();

            this.canal = session.openChannel("sftp");
            this.canal.connect();
            this.canalsftp = (ChannelSftp)canal;
		}
		
		public void Desconectar(){
            try
            {
                if (this.session.isConnected()) this.session.disconnect();
                if (this.canalsftp.isConnected()) this.canalsftp.disconnect();
            }
            catch { }
		}
		
		public void Get(string partida, string destino){
            if (partida == null)
            {
                throw new ArgumentNullException("partida");
            }

            if (partida.Equals(string.Empty))
            {
                throw new ArgumentException("partida invalida");
            }

            if (destino == null)
            {
                throw new ArgumentNullException("destino");
            }

            if (destino.Equals(string.Empty))
            {
                throw new ArgumentException("destino invalido");
            }

			try
            {
                monitor = new MyProgressMonitor();
				this.Conectar();
				this.canalsftp.get(partida, destino, monitor);			
			} 
            finally{
				this.Desconectar();
			}
		}

        public void Put(string partida, string destino)
        {
            if (partida == null)
            {
                throw new ArgumentNullException("partida");
            }

            if (partida.Equals(string.Empty))
            {
                throw new ArgumentException("partida invalida");
            }

            if (destino == null)
            {
                throw new ArgumentNullException("destino");
            }

            if (destino.Equals(string.Empty))
            {
                throw new ArgumentException("destino invalido");
            }

            try
            {
                monitor = new MyProgressMonitor();
                this.Conectar();
                this.canalsftp.put(partida, destino, monitor, ChannelSftp.OVERWRITE);
            }
            finally
            {
                this.Desconectar();
            }
        }

        public void Put(ArrayList lista_Partida, string destino)
        {
            if (lista_Partida == null)
            {
                throw new ArgumentNullException("lista_partida");
            }

            if (destino == null)
            {
                throw new ArgumentNullException("destino");
            }

            if (destino.Equals(string.Empty))
            {
                throw new ArgumentException("destino invalido");
            }

            try
            {
                this.Conectar();
                foreach (string partidas in lista_Partida)
                {
                    if (partidas != null)
                    {
                        if (!partidas.Equals(string.Empty))
                        {
                            monitor = new MyProgressMonitor();
                            this.canalsftp.put(partidas, destino, monitor, ChannelSftp.OVERWRITE);
                        }
                        else
                        {
                            throw new ArgumentException("partida invalida");
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("partida");
                    }
                }
            }
            finally
            {
                this.Desconectar();
            }
        }
        #endregion
    }
        
}
