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
                writer.WriteElementString("InventoryNum", asset.InventoryNumber);
                writer.WriteElementString("AssetName", asset.Name.ToString());
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
            XDocument xdoc = XDocument.Load("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Asset.xml");
            XElement? root = xdoc.Element("Assets");

            if (root != null)
            {
                root.Add(new XElement("Asset",
                            new XElement("Id", asset.Id.ToString()),
                            new XElement("InventoryNum", asset.InventoryNumber),
                            new XElement("AssetName", asset.Name.ToString()),
                            new XElement("InitialCost", asset.InitialCost.ToString()),
                            new XElement("DepartmentId", asset.DepartmentId.ToString()),
                            new XElement("ResponsiblePersonId", asset.ResponsiblePersonId.ToString())));

                xdoc.Save("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Asset.xml");
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

            using (FileStream fs = new FileStream("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Asset.xml", FileMode.OpenOrCreate))
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
                using (FileStream fs = new FileStream("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Asset.xml", FileMode.Create))
                {
                    existingAssets.AddRange(newAssetList);
                    formatter.Serialize(fs, existingAssets);
                }
            }
        }
    }
}
