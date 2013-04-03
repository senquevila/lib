/*
 * Creado por SharpDevelop.
 * Usuario: jeavila
 * Fecha: 26/05/2009
 * Hora: 05:59 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */

using System;
using System.Collections;
using System.Xml;

namespace SIAFI
{
	/// <summary>
	/// Description of xmlClass.
	/// </summary>
	public class Xml
	{
		private static ArrayList Id = new ArrayList();
		private static ArrayList Nodo = new ArrayList();
		private static ArrayList Contenido = new ArrayList();
		private string archivo;
		private XmlDocument documento = new XmlDocument();
		
		public Xml(string archivo)
		{
			this.archivo = archivo;
		}
		
		#region lectura
		public ArrayList leerPorNodo(string nodo, string subnodo){
			ArrayList aux = new ArrayList();
			
			documento.Load(this.archivo);
			XmlNodeList Nodo = documento.GetElementsByTagName(nodo);
			XmlNodeList lista = ((XmlElement)Nodo[0]).GetElementsByTagName(subnodo);
			foreach (XmlElement elemento in lista){
				try{
					aux.Add(elemento.InnerText);
				} catch{ return null; }
			}
			return aux;
		}
		
		public ArrayList leerPorAtributo(string nodo, string subnodo, string[] atributos){
			ArrayList aux = new ArrayList();
			try{
				documento.Load(this.archivo);
				XmlNodeList Nodo = documento.GetElementsByTagName(nodo);
				XmlNodeList lista = ((XmlElement)Nodo[0]).GetElementsByTagName(subnodo);
				foreach (XmlElement elemento in lista){
					
					string auxStr = null;
					foreach(string str in atributos){
						auxStr += elemento.GetAttribute(str) + ";";
					}
					auxStr = auxStr.Substring(0, auxStr.Length-1);
					aux.Add(auxStr);
				}
				return aux;
			} catch{ return null; }
		}
		#endregion
		
		#region escribir
		private XmlNode crearNodoPorNombre(string nombre_elemento, Hashtable datos)
		{
			IEnumerator enumerador = datos.GetEnumerator();
			
			//Creamos el nodo que deseamos insertar.
			XmlElement elemento = documento.CreateElement(nombre_elemento);

			// Recorrer el hashtable e insertar en el elemento
			foreach(string key in datos.Keys){
				XmlElement aux = documento.CreateElement(key);
				aux.InnerText = datos[key].ToString();
				elemento.AppendChild(aux);
			}
			return elemento;
		}
		
		private XmlNode crearNodoPorAtributo(string nombre_elemento, Hashtable datos){
			XmlElement elemento = documento.CreateElement(nombre_elemento);
			
			foreach(string key in datos.Keys){
				elemento.SetAttribute(key, datos[key].ToString());
			}
			
			return elemento;
		}
		
		public void insertarNodo(string nombre, Hashtable hs, char tipo)
		{
			XmlNode nodo = null;
			
			//Cargamos el documento XML.
			documento.Load(this.archivo);

			//Creamos el nodo que deseamos insertar.
			switch(tipo){
				case 'n' : case 'N':
					nodo = this.crearNodoPorNombre(nombre, hs);
					break;
				case 't': case 'T':
					nodo = this.crearNodoPorAtributo(nombre, hs);
					break;
			}
			
			if (nodo != null){
				//Obtenemos el nodo raiz del documento.
				XmlNode nodoRaiz = documento.DocumentElement;

				//Insertamos el nodo elemento al final del archivo
				nodoRaiz.InsertAfter(nodo, nodoRaiz.LastChild);

				// Salvar el documento
				documento.Save(this.archivo);
			}
		}
		
		public void actualizarNodo(string nombre, Hashtable hs, char tipo){
			XmlNode nuevoNodo = null;
			documento.Load(this.archivo);
			
			switch(tipo){
				case 'n' : case 'N':
					nuevoNodo = this.crearNodoPorNombre(nombre, hs);
					break;
				case 't': case 'T':
					nuevoNodo = this.crearNodoPorAtributo(nombre, hs);
					break;
			}
			
			if (nuevoNodo != null){
				//ReplaceChild(nuevoNodo, viejoNodo);
			}
			
		}
		#endregion
		
		#region funciones propias
		public void addNode(int id, string nodo, Object valor){
			Id.Add(id);
			Nodo.Add(nodo);
			Contenido.Add(valor);
		}
		
		public void clearNodes() {
			Id.Clear();
			Nodo.Clear();
			Contenido.Clear();
		}
		
		public void write(){
			Fichero f = new Fichero(this.archivo);
			int cierre = 0;
			//cabecera
			f.escribir_todo("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
			for(int i=0; i<Contenido.Count; i++){
				if(entero(i) < entero(i+1)){
					f.escribir(bracket(i,0) + "\n");
					cierre = i;
				} else if(entero(i) == entero(i+1) || i==Contenido.Count-1){
					f.escribir(bracket(i,0) + Contenido[i].ToString() + bracket(i,2));
				}
			}
			//cierres
			for(int i=cierre; i>=0; i--)
				f.escribir(bracket(i, 1));
		}
		
		private string sangria(int num){
			string aux = "";
			for(int i=0; i<num; i++) aux += "\t";
			return aux;
		}
		
		private int entero(int id){
			int aux = 0;
			try{
				aux = Convert.ToInt16(Id[id].ToString());
			} catch(Exception){ aux = -1; }
			return aux;
		}
		
		private string bracket(int i, int tipo){
			switch(tipo){
				case 0:
					return sangria(entero(i)) + "<" + Nodo[i].ToString() + ">";
				case 1: default:
					return sangria(i) + "</" + Nodo[i].ToString() + ">\n";
				case 2:
					return "</" + Nodo[i].ToString() + ">\n";
				case 3:
					return " />\n";
			}
		}
		#endregion
		
	}
}
