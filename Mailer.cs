/*
 * Creado por SharpDevelop.
 * Usuario: jeavila
 * Fecha: 25/05/2009
 * Hora: 02:57 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */

namespace Senque
{
    using System;
    using System.Collections;
    using System.Net.Mail;
    using System.IO;

	/// <summary>
	/// Clase para enviar correos
	/// </summary> 
	public class Mailer
	{		
        /// <summary>
        /// Mensaje de correo
        /// </summary>
		private MailMessage msg;

        /// <summary>
        /// Cliente SMTP
        /// </summary>
		private SmtpClient cliente;
		
        /// <summary>
        /// Initializes a new instance of the Mailer class => detail mode
        /// </summary>
        /// <param name="titulo">Titulo del correo</param>
        /// <param name="cuerpo">Contenido del correo</param>
        /// <param name="host">Direccion del servidor de correos</param>
        /// <param name="from">FROM del correo</param>
        /// <param name="to">TO del correo</param>
        /// <param name="cc">CC del correo</param>
        /// <param name="bcc">BCC del correo</param>
		public Mailer(string titulo, string cuerpo, string host, string from, ArrayList to, ArrayList cc, ArrayList bcc)
		{			
			this.msg = new MailMessage();

            if (titulo != null && titulo != string.Empty)
            {
                this.msg.Subject = titulo;
            }
            this.msg.Body = cuerpo;

            if (host != null && host != string.Empty)
            {
                this.cliente = new SmtpClient(host);
            }
            else
            {
                throw new ArgumentNullException("host");
            }

            if (from != null && from != string.Empty)
            {
                this.msg.From = new MailAddress(from);
            }
            else
            {
                throw new ArgumentNullException("from");
            }

            if (to != null)
            {
                foreach (string valor in to)
                {
                    this.msg.To.Add(valor);
                }
            }
            else
            {
                throw new ArgumentNullException("to");
            }

            if (cc != null)
            {
                foreach (string valor in cc) 
                { 
                    this.msg.CC.Add(valor); 
                }
            }

            if (bcc != null)
            {
                foreach (string valor in bcc) 
                { 
                    this.msg.Bcc.Add(valor); 
                }
            }
		}

        /// <summary>
        /// Initializes a new instance of the Mailer class => reduce mode
        /// </summary>
        /// <param name="contenido">Almacena el titulo, cuerpo, ip del servidor y FROM del correo</param>
        /// <param name="to">TO del correo</param>
        /// <param name="cc">CC del correo</param>
        /// <param name="bcc">BCC del correo</param>
        public Mailer(Hashtable contenido, ArrayList to, ArrayList cc, ArrayList bcc)
        {
            if (contenido != null)
            {
                this.msg = new MailMessage();

                if (contenido["host"] != null)
                {
                    if (!contenido["host"].ToString().Equals(string.Empty))
                    {
                        this.cliente = new SmtpClient(contenido["host"].ToString());
                    }
                    else
                    {
                        throw new ArgumentException("Contenido_host invalido");
                    }
                }
                else
                {
                    throw new ArgumentNullException("contenido");
                }

                if (contenido["from"] != null)
                {
                    if (!contenido["from"].ToString().Equals(string.Empty))
                    {
                        this.msg.From = new MailAddress(contenido["from"].ToString());
                    }
                    else
                    {
                        throw new ArgumentException("Contenido_from invalido");
                    }
                }
                else
                {
                    throw new ArgumentNullException("contenido");
                }

                if (contenido["titulo"] != null)
                {
                    if (!contenido["titulo"].ToString().Equals(string.Empty))
                    {
                        this.msg.Subject = contenido["titulo"].ToString();
                    }
                    else
                    {
                        throw new ArgumentException("contenido_titulo invalido");
                    }
                }
                else
                {
                    throw new ArgumentNullException("contenido");
                }

                if (contenido["cuerpo"] != null)
                {
                    if (!contenido["cuerpo"].ToString().Equals(string.Empty))
                    {
                        this.msg.Body = contenido["cuerpo"].ToString();
                    }
                    else
                    {
                        throw new ArgumentException("contenido_cuerpo invalido");
                    }
                }
                else
                {
                    throw new ArgumentNullException("contenido");
                }

                if (to != null)
                {
                    foreach (string valor in to)
                    {
                        this.msg.To.Add(valor);
                    }
                }
                else
                {
                    throw new ArgumentNullException("to");
                }

                if (cc != null)
                {
                    foreach (string valor in cc)
                    {
                        this.msg.CC.Add(valor);
                    }
                }

                if (bcc != null)
                {
                    foreach (string valor in bcc)
                    {
                        this.msg.Bcc.Add(valor);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("contenido");
            }
        }
		
        /// <summary>
        /// Envia el correo
        /// </summary>
        /// <returns>Boolean or bool</returns>        		
        public bool Enviar()
        {
			try
            {
				this.cliente.Send(this.msg);
				return true;
			} 
            catch
            { 
                return false; 
            }
		}
		
        /// <summary>
        /// Agrega archivos adjuntos al correo
        /// </summary>
        /// <param name="attach">Lista de las url de los archivos a adjuntar</param>
		public void Adjuntar(ArrayList attach)
        {
            if (attach != null)
            {
                foreach (string valor in attach)
                {
                    if (File.Exists(valor))
                    {
                        Attachment adj = new Attachment(valor);
                        this.msg.Attachments.Add(adj);
                    }
                    else
                    {
                        throw new ArgumentException("Ruta de archivo invalida");
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("attach");
            }
		}
	}
}
