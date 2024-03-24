using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace Lab2_.Net_.AddXMLDocument
{
    public class ResponsiblePersonXMLDocument
    {
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

        public void AddXMLElement(ResponsiblePerson person)
        {
            XDocument xdoc = XDocument.Load("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\ResponsiblePerson.xml");
            XElement? root = xdoc.Element("ResponsiblePersons");

            if (root != null)
            {
                root.Add(new XElement("ResponsiblePerson",
                            new XElement("Id", person.Id.ToString()),
                            new XElement("Name", person.Name),
                            new XElement("Surname", person.Surname),
                            new XElement("Phone", person.Phone)));

                xdoc.Save("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\ResponsiblePerson.xml");
            }

            Console.WriteLine("XML елемент успішно");
        }
    }
}
