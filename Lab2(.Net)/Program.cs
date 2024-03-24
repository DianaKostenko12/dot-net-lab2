using Lab2_.Net_.AddXMLDocument;
using Lab2_.Net_.Models;
using System;
using System.Xml;

namespace Lab2_.Net_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer = "";
            AssetXMLDocument xmlDocument = new AssetXMLDocument();
            DepartmentXMLDocument departmentXML = new DepartmentXMLDocument();
            DocumentXMLDocument documentXMLDocument = new DocumentXMLDocument();
            ResponsiblePersonXMLDocument responsiblePersonXMLDocument = new ResponsiblePersonXMLDocument();
            ResponsiblePerson responsiblePerson;
            Asset asset;
            Department department;
            Document document;
            do
            {
                Console.Clear();
                Console.WriteLine("Що будемо робити? \n" +
                    "\n1-Додати новий об'єкт Asset до XML документу?" +
                    "\n2-Додати новий об'єкт Department до XML документу" +
                    "\n3-Додати новий об'єкт Document до XML документу" +
                    "\n4-Додати новий об'єкт ResponsiblePerson до XML документу");

                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        asset = Asset.CreateAsset();
                        xmlDocument.AddXMLElement(asset);
                        break;
                    case 2:
                        department = Department.CreateDepartment();
                        departmentXML.AddXMLElement(department);
                        break;
                    case 3:
                        document = Document.CreateDocument();
                        documentXMLDocument.AddXMLElement(document);
                        break;
                    case 4:
                        responsiblePerson = ResponsiblePerson.CreateResponsiblePerson();
                        responsiblePersonXMLDocument.AddXMLElement(responsiblePerson);
                        break;
                }
                Console.WriteLine("Бажаєте продовжити? (+/-)");
                answer = Console.ReadLine();

            }
            while (answer == "+");
        }
    }
}
