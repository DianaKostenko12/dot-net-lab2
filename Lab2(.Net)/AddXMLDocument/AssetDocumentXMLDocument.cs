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
        private readonly string _dirPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\AssetDocument.xml";
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
                writer.WriteElementString("Date", XmlConvert.ToString(assetDocument.Date));
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }

            Console.WriteLine("XML файл успішно створений");
        }

        public void AddXMLElement(AssetDocument assetDocument)
        {
            XDocument xdoc = XDocument.Load(_dirPath);
            XElement? root = xdoc.Element("AssetDocuments");

            if (root != null)
            {
                root.Add(new XElement("AssetDocument",
                            new XElement("AssetId", assetDocument.AssetId.ToString()),
                            new XElement("DocumentId", assetDocument.DocumentId.ToString()),
                            new XElement("Date", XmlConvert.ToString(assetDocument.Date))));

                xdoc.Save(_dirPath);
            }

            Console.WriteLine("XML елемент успішно доданий");
        }

        public void AddCollection()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "AssetDocuments";
            xRoot.IsNullable = true;

            XmlSerializer formatter = new XmlSerializer(typeof(List<AssetDocument>), xRoot);

            List<AssetDocument> existingAssetDocuments;

            using (FileStream fs = new FileStream(_dirPath, FileMode.OpenOrCreate))
            {
                existingAssetDocuments = formatter.Deserialize(fs) as List<AssetDocument>;
                
            }
            
            using (FileStream fs = new FileStream(_dirPath, FileMode.Create))
            {
                existingAssetDocuments.AddRange(_dataFilling.assetDocuments);
                if(existingAssetDocuments != null)
                {
                    formatter.Serialize(fs, existingAssetDocuments);
                }
            }
        }
        public void ReturnAllAssetDocuments()
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
                        if (childnode.Name == "AssetId")
                        {
                            Console.WriteLine($"Id основного засобу: {childnode.InnerText}");
                        }

                        if (childnode.Name == "DocumentId")
                        {
                            Console.WriteLine($"Id документу: {childnode.InnerText}");
                        }

                        if (childnode.Name == "Date")
                        {
                            Console.WriteLine($"Дата проведення операції: {childnode.InnerText}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

    }
}
