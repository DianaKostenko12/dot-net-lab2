using Lab2_.Net_.AddXMLDocument;
using Lab2_.Net_.Models;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Lab2_.Net_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer = "";
            DataFilling dataFilling = new DataFilling();
            AssetXMLDocument xmlDocument = new AssetXMLDocument(dataFilling);
            DepartmentXMLDocument departmentXML = new DepartmentXMLDocument(dataFilling);
            DocumentXMLDocument documentXMLDocument = new DocumentXMLDocument();
            ResponsiblePersonXMLDocument responsiblePersonXMLDocument = new ResponsiblePersonXMLDocument();
            AssetDocumentXMLDocument assetDocumentXMLDocument = new AssetDocumentXMLDocument(dataFilling);
            ResponsiblePerson responsiblePerson;
            Asset asset;
            Department department;
            Document document;
            AssetDocument assetDocument;
            do
            {
                Console.Clear();
                Console.WriteLine("Що будемо робити? \n" +
                    "\n1-Додати новий об'єкт Asset до XML документу?" +
                    "\n2-Додати новий об'єкт Department до XML документу" +
                    "\n3-Додати новий об'єкт Document до XML документу" +
                    "\n4-Додати новий об'єкт ResponsiblePerson до XML документу" +
                    "\n5-Додати новий об'єкт AssetDocument до XML документу" +
                    "\n6-Додати список об'єктів Asset до XML документу" +
                    "\n7-Додати список об'єктів Department до XML документу");

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
                    case 5:
                        assetDocument = AssetDocument.CreateAssetDocument();
                        assetDocumentXMLDocument.AddXMLElement(assetDocument);
                        break;
                    case 6:
                        xmlDocument.AddCollection();
                        break;
                    case 7:
                        departmentXML.AddCollection();
                        break;
                }
                Console.WriteLine("Бажаєте продовжити? (+/-)");
                answer = Console.ReadLine();

            }
            while (answer == "+");
        }
    }
}
