using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lab2_.Net_.AddXMLDocument
{
    public class AssetDocumentXMLDocument
    {
        private readonly DataFilling _dataFilling;
        public AssetDocumentXMLDocument(DataFilling dataFilling)
        {
            _dataFilling = dataFilling;
        }
        public void CreateXMLDocument(AssetDocument assetDocument)
        {
            string projectDirectory = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)";
            string outputPath = System.IO.Path.Combine(projectDirectory, "AssetDocument.xml");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("AssetDocuments");

                writer.WriteStartElement("AssetDocument");
                writer.WriteElementString("AssetId", assetDocument.AssetId.ToString());
                writer.WriteElementString("DocumentId", assetDocument.DocumentId.ToString());
                writer.WriteElementString("Date", assetDocument.Date.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }

            Console.WriteLine("XML файл успішно створений");
        }

        public void AddXMLElement(AssetDocument assetDocument)
        {
            XDocument xdoc = XDocument.Load("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\AssetDocument.xml");
            XElement? root = xdoc.Element("AssetDocuments");

            if (root != null)
            {
                root.Add(new XElement("AssetDocument",
                            new XElement("AssetId", assetDocument.AssetId.ToString()),
                            new XElement("DocumentId", assetDocument.DocumentId.ToString()),
                            new XElement("Date", assetDocument.Date.ToString())));

                xdoc.Save("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\AssetDocument.xml");
            }

            Console.WriteLine("XML елемент успішно доданий");
        }
    }
}
