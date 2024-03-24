using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Document = Lab2_.Net_.Models.Document;

namespace Lab2_.Net_.AddXMLDocument
{
    public class DocumentXMLDocument
    {
        public void CreateXMLDocument(Document document)
        {
            string projectDirectory = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)";
            string outputPath = System.IO.Path.Combine(projectDirectory, "Document.xml");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Documents");

                writer.WriteStartElement("Document");
                writer.WriteElementString("Id", document.Id.ToString());
                writer.WriteElementString("DocumentName", document.DocumentName);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }

            Console.WriteLine("XML файл успішно створений");
        }

        public void AddXMLElement(Document document)
        {
            XDocument xdoc = XDocument.Load("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Document.xml");
            XElement? root = xdoc.Element("Documents");

            if (root != null)
            {
                root.Add(new XElement("Document",
                            new XElement("Id", document.Id.ToString()),
                            new XElement("DepartmentName", document.DocumentName)));

                xdoc.Save("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Document.xml");
            }

            Console.WriteLine("XML елемент успішно доданий");
        }
    }
}
