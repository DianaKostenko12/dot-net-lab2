using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Lab2_.Net_.AddXMLDocument
{
    public class DepartmentXMLDocument
    {
        public void CreateXMLDocument(Department department)
        {
            string projectDirectory = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)";
            string outputPath = System.IO.Path.Combine(projectDirectory, "Department.xml");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Departments");

                writer.WriteStartElement("Department");
                writer.WriteElementString("Id", department.Id.ToString());
                writer.WriteElementString("DepartmentName", department.DepartmentName);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }

            Console.WriteLine("XML файл успішно створений");
        }

        public void AddXMLElement(Department department)
        {
            XDocument xdoc = XDocument.Load("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Department.xml");
            XElement? root = xdoc.Element("Departments");

            if (root != null)
            {
                root.Add(new XElement("Department",
                            new XElement("Id", department.Id.ToString()),
                            new XElement("DepartmentName", department.DepartmentName)));

                xdoc.Save("D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Department.xml");
            }

            Console.WriteLine("XML елемент успішно доданий");
        }
    }
}
