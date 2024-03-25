using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab2_.Net_.AddXMLDocument
{
    public class AssetXMLDocument
    {
        private readonly DataFilling _dataFilling;
        private readonly string _dirPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Asset.xml";
        public AssetXMLDocument(DataFilling dataFilling) 
        {
            _dataFilling = dataFilling;
        }

        public void CreateXMLDocument(Asset asset)
        {
            string projectDirectory = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)";
            string outputPath = System.IO.Path.Combine(projectDirectory, "Asset.xml");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Assets");

                writer.WriteStartElement("Asset");
                writer.WriteElementString("Id", asset.Id.ToString());
                writer.WriteElementString("InventoryNumber", asset.InventoryNumber);
                writer.WriteElementString("Name", asset.Name.ToString());
                writer.WriteElementString("InitialCost", asset.InitialCost.ToString());
                writer.WriteElementString("DepartmentId", asset.DepartmentId.ToString());
                writer.WriteElementString("ResponsiblePersonId", asset.ResponsiblePersonId.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement(); 
                writer.WriteEndDocument();
                writer.Flush();

            }

            Console.WriteLine("XML файл успішно створений");
        }

        public void AddXMLElement(Asset asset)
        {
            XDocument xdoc = XDocument.Load(_dirPath);
            XElement? root = xdoc.Element("Assets");

            if (root != null)
            {
                root.Add(new XElement("Asset",
                            new XElement("Id", asset.Id.ToString()),
                            new XElement("InventoryNumber", asset.InventoryNumber),
                            new XElement("Name", asset.Name.ToString()),
                            new XElement("InitialCost", asset.InitialCost.ToString()),
                            new XElement("DepartmentId", asset.DepartmentId.ToString()),
                            new XElement("ResponsiblePersonId", asset.ResponsiblePersonId.ToString())));

                xdoc.Save(_dirPath);
            }

            Console.WriteLine("XML елемент успішно доданий");
        }

        public void AddCollection()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Assets";
            xRoot.IsNullable = true;

            XmlSerializer formatter = new XmlSerializer(typeof(List<Asset>), xRoot);

            List<Asset> newAssetList = new List<Asset>();

            List<Asset> existingAssets;

            using (FileStream fs = new FileStream(_dirPath, FileMode.OpenOrCreate))
            {
                existingAssets = formatter.Deserialize(fs) as List<Asset>;
            }

            foreach (var asset in _dataFilling.assets)
            {
                if (!existingAssets.Any(a => a.Id == asset.Id))
                {
                    newAssetList.Add(asset);
                }
            }

            if (newAssetList.Count > 0)
            {
                using (FileStream fs = new FileStream(_dirPath, FileMode.Create))
                {
                    existingAssets.AddRange(newAssetList);
                    formatter.Serialize(fs, existingAssets);
                }
            }
        }

        public void ReturnAllAssets()
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

                        if (childnode.Name == "InventoryNumber")
                        {
                            Console.WriteLine($"Інвентарний номер: {childnode.InnerText}");
                        }

                        if (childnode.Name == "Name")
                        {
                            Console.WriteLine($"Назва основного засобу: {childnode.InnerText}");
                        }

                        if (childnode.Name == "InitialCost")
                        {
                            Console.WriteLine($"Первісна вартість: {childnode.InnerText}");
                        }

                        if (childnode.Name == "DepartmentId")
                        {
                            Console.WriteLine($"Id відділу: {childnode.InnerText}");
                        }

                        if (childnode.Name == "ResponsiblePersonId")
                        {
                            Console.WriteLine($"Id відповідальної особи: {childnode.InnerText}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
