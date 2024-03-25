using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Document = Lab2_.Net_.Models.Document;

namespace Lab2_.Net_.AddXMLDocument
{
    public class DocumentXMLDocument
    {
        private readonly DataFilling _dataFilling;
        private readonly string _dirPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Document.xml";
        public DocumentXMLDocument(DataFilling dataFilling)
        {
            _dataFilling = new DataFilling();
        }
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

        public void AddXmlElement(Document document)
        {
            XDocument xdoc = XDocument.Load(_dirPath);
            XElement? root = xdoc.Element("Documents");

            if (root != null)
            {
                root.Add(new XElement("Document",
                            new XElement("Id", document.Id.ToString()),
                            new XElement("DocumentName", document.DocumentName)));

                xdoc.Save(_dirPath);
            }

            Console.WriteLine("XML елемент успішно доданий");
        }

        public void AddCollection()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Documents";
            xRoot.IsNullable = true;

            XmlSerializer formatter = new XmlSerializer(typeof(List<Document>), xRoot);

            List<Document> newDocumentList = new List<Document>();

            List<Document> existingDocuments;

            using (FileStream fs = new FileStream(_dirPath, FileMode.OpenOrCreate))
            {
                existingDocuments = formatter.Deserialize(fs) as List<Document>;
            }

            foreach (var document in _dataFilling.documents)
            {
                if (!existingDocuments.Any(a => a.Id == document.Id))
                {
                    newDocumentList.Add(document);
                }
            }

            if (newDocumentList.Count > 0)
            {
                using (FileStream fs = new FileStream(_dirPath, FileMode.Create))
                {
                    existingDocuments.AddRange(newDocumentList);
                    formatter.Serialize(fs, existingDocuments);
                }
            }
            Console.WriteLine("Об'єкти успішно додані");
        }

        public void ReturnAllDocuments()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_dirPath);

            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "Id")
                        {
                            Console.WriteLine($"Id: {childnode.InnerText}");
                        }

                        if (childnode.Name == "DocumentName")
                        {
                            Console.WriteLine($"Назва документу: {childnode.InnerText}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
