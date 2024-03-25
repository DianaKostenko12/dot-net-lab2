using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Lab2_.Net_.AddXMLDocument
{
    public class ResponsiblePersonXMLDocument
    {
        private readonly DataFilling _dataFilling;
        private readonly string _dirPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\ResponsiblePerson.xml";
        public ResponsiblePersonXMLDocument(DataFilling dataFilling)
        {
            _dataFilling = dataFilling;
        }

        public void CreateXMLDocument(ResponsiblePerson person)
        {
            string projectDirectory = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)";
            string outputPath = System.IO.Path.Combine(projectDirectory, "ResponsiblePerson.xml");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ResponsiblePersons");
                Console.WriteLine("Введiть ID відповідальної персони:");

                writer.WriteStartElement("ResponsiblePerson");
                writer.WriteElementString("Id", person.Id.ToString());
                writer.WriteElementString("Name", person.Name);
                writer.WriteElementString("Surname", person.Surname);
                writer.WriteElementString("Phone", person.Phone);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }

            Console.WriteLine("XML файл успішно створений");
        }

        public void AddXmlElement(ResponsiblePerson person)
        {
            XDocument xdoc = XDocument.Load(_dirPath);
            XElement? root = xdoc.Element("ResponsiblePersons");

            if (root != null)
            {
                root.Add(new XElement("ResponsiblePerson",
                            new XElement("Id", person.Id.ToString()),
                            new XElement("Name", person.Name),
                            new XElement("Surname", person.Surname),
                            new XElement("Phone", person.Phone)));

                xdoc.Save(_dirPath);
            }

            Console.WriteLine("XML елемент успішно");
        }

        public void AddCollection()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "ResponsiblePersons";
            xRoot.IsNullable = true;

            XmlSerializer formatter = new XmlSerializer(typeof(List<ResponsiblePerson>), xRoot);

            List<ResponsiblePerson> newResponsiblePersonList = new List<ResponsiblePerson>();

            List<ResponsiblePerson> existingResponsiblePersons;

            using (FileStream fs = new FileStream(_dirPath, FileMode.OpenOrCreate))
            {
                existingResponsiblePersons = formatter.Deserialize(fs) as List<ResponsiblePerson>;
            }

            foreach (var responsiblePerson in _dataFilling.responsiblePersons)
            {
                if (!existingResponsiblePersons.Any(a => a.Id == responsiblePerson.Id))
                {
                    newResponsiblePersonList.Add(responsiblePerson);
                }
            }

            if (newResponsiblePersonList.Count > 0)
            {
                using (FileStream fs = new FileStream(_dirPath, FileMode.Create))
                {
                    existingResponsiblePersons.AddRange(newResponsiblePersonList);
                    formatter.Serialize(fs, existingResponsiblePersons);
                }
            }

            Console.WriteLine("Об'єкти успішно додані");
        }

        public void ReturnAllResponsiblePersons()
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

                        if (childnode.Name == "Name")
                        {
                            Console.WriteLine($"Ім'я відповідальної персони: {childnode.InnerText}");
                        }

                        if (childnode.Name == "Surname")
                        {
                            Console.WriteLine($"Прізвище відповідальної персони: {childnode.InnerText}");
                        }

                        if (childnode.Name == "Phone")
                        {
                            Console.WriteLine($"Телефон відповідальної персони: {childnode.InnerText}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
