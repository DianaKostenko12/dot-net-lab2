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
    public class DepartmentXMLDocument
    {
        private readonly DataFilling _dataFilling;
        private readonly string _dirPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Department.xml";
        public DepartmentXMLDocument(DataFilling dataFilling)
        {
            _dataFilling = new DataFilling();
        }
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

            Console.WriteLine("XML файл успiшно створений");
        }

        public void AddXMLElement(Department department)
        {
            XDocument xdoc = XDocument.Load(_dirPath);
            XElement? root = xdoc.Element("Departments");

            if (root != null)
            {
                root.Add(new XElement("Department",
                            new XElement("Id", department.Id.ToString()),
                            new XElement("DepartmentName", department.DepartmentName)));

                xdoc.Save(_dirPath);
            }

            Console.WriteLine("XML елемент успiшно доданий");
        }

        public void AddCollection()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Departments";
            xRoot.IsNullable = true;

            XmlSerializer formatter = new XmlSerializer(typeof(List<Department>), xRoot);

            List<Department> newDepartmentList = new List<Department>();

            List<Department> existingDepartments;

            using (FileStream fs = new FileStream(_dirPath, FileMode.OpenOrCreate))
            {
                existingDepartments = formatter.Deserialize(fs) as List<Department>;
            }

            existingDepartments.RemoveAll(d => true); 

            foreach (var department in _dataFilling.departments)
            {
                newDepartmentList.Add(department);
            }

            using (FileStream fs = new FileStream(_dirPath, FileMode.Create))
            {
                formatter.Serialize(fs, newDepartmentList);
            }

            Console.WriteLine("Об'єкти успiшно доданi");

        }

        public void ReturnAllDepartments()
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

                        if (childnode.Name == "DepartmentName")
                        {
                            Console.WriteLine($"Назва вiддiлу: {childnode.InnerText}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
