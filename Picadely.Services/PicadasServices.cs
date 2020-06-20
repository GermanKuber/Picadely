using Picadely.Entities;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Picadely.Services
{
    public class PicadasServices
    {
        public List<Picada> GetPicadas()
        {
            var sqlService = new SqlAccessService();

            var dataTable = sqlService.SelectDatas("Picadas", new List<string> { "Id", "Nombre", "Comensales" });

            var picadas = new List<Picada>();
            foreach (DataRow row in dataTable.Rows)
            {
                picadas.Add(new Picada
                {

                    Nombre = row["Nombre"].ToString(),
                    Comensales = int.Parse(row["Comensales"].ToString()),
                    Id = int.Parse(row["Id"].ToString())
                });
            }
            GenerarMenuEnXml(picadas);

            return picadas;
        }
        public List<int> GetComensalesFromXml()
        {
            //Recuepera las picadas desde el xml y con xpath filtra los comensales
            XPathDocument doc = new XPathDocument(HttpContext.Current.Server.MapPath("document.xml"));
            XPathNavigator xPathNavigator = doc.CreateNavigator();
            XPathNodeIterator iterator = xPathNavigator.Select("Catalogo/Picada/@Comensales");

            var comensales = new List<int>();
            foreach (XPathNavigator item in iterator)
                comensales.Add(int.Parse(item.Value));

            return comensales;
        }
        private void GenerarMenuEnXml(List<Picada> picadas)
        {
            //A partir de una lista de picadas genera un xml con el menu.
            //EJ:
            //<?xml version="1.0" encoding="UTF-8"?>
            //<Catalogo>
            //  <Picada Id="4" Comensales="3">Quesos</Picada>
            //  <Picada Id="5" Comensales="12">Quesos Grandes</Picada>
            //</Catalogo>
            var doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element1 = doc.CreateElement(string.Empty, "Catalogo", string.Empty);
            doc.AppendChild(element1);

            foreach (var picada in picadas)
            {
                XmlElement element2 = doc.CreateElement(string.Empty, "Picada", string.Empty);

                element2.SetAttribute("Id", picada.Id.ToString());

                element2.SetAttribute("Comensales", picada.Comensales.ToString());
                XmlText text1 = doc.CreateTextNode(picada.Nombre);
                element2.AppendChild(text1);
                element1.AppendChild(element2);
            }

            doc.Save(@"C:\picada\picada.xml");
        }
    }

    public class LogServices
    {
        public List<Log> Get()
        {
            var sqlService = new SqlAccessService();

            var dataTable = sqlService.SelectDatas("Logs", new List<string> { "Id", "Tipo", "Fecha", "Email", "Descripcion", "Digito" });

            var logs = new List<Log>();
            foreach (DataRow row in dataTable.Rows)
            {
                logs.Add(new Log
                {

                    Email = row["Email"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    Fecha = row["Fecha"].ToString(),
                    Digito = row["Digito"].ToString(),
                    Tipo = row["Tipo"].ToString(),
                });
            }
            return logs;

        }
    }
}
