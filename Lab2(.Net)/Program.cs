using Lab2_.Net_.AddXMLDocument;
using Lab2_.Net_.Models;
using System;
using System.Reflection.Metadata;
using System.Xml;
using System.Xml.Linq;
using Document = Lab2_.Net_.Models.Document;

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
            DocumentXMLDocument documentXMLDocument = new DocumentXMLDocument(dataFilling);
            ResponsiblePersonXMLDocument responsiblePersonXMLDocument = new ResponsiblePersonXMLDocument(dataFilling);
            AssetDocumentXMLDocument assetDocumentXMLDocument = new AssetDocumentXMLDocument(dataFilling);
            ResponsiblePerson responsiblePerson;
            Asset asset;
            Department department;
            Document document;
            AssetDocument assetDocument;
            do
            {
                Console.Clear();
                Console.WriteLine("Що ви хочете зробити? \n" +
                    "\n1-Додати новий об'єкт Asset до XML документу?" +
                    "\n2-Додати новий об'єкт Department до XML документу" +
                    "\n3-Додати новий об'єкт Document до XML документу" +
                    "\n4-Додати новий об'єкт ResponsiblePerson до XML документу" +
                    "\n5-Додати новий об'єкт AssetDocument до XML документу" +
                    "\n6-Додати список об'єктів Asset до XML документу" +
                    "\n7-Додати список об'єктів Department до XML документу" +
                    "\n8-Додати список об'єктів Document до XML документу" +
                    "\n9-Додати список об'єктів ResponsiblePerson до XML документу" +
                    "\n10-Додати список об'єктів AssetDocument до XML документу" +
                    "\n11- Вивести список об'єктів Assets з XML файлу" +
                    "\n12- Вивести список об'єктів Departments з XML файлу" +
                    "\n13- Вивести список об'єктів Documents з XML файлу" +
                    "\n14- Вивести список об'єктів ResponsiblePersons з XML файлу" +
                    "\n15- Вивести список об'єктів AssetDocuments з XML файлу" +
                    "\n16 - Показати запити над даними ");

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
                        documentXMLDocument.AddXmlElement(document);
                        break;
                    case 4:
                        responsiblePerson = ResponsiblePerson.CreateResponsiblePerson();
                        responsiblePersonXMLDocument.AddXmlElement(responsiblePerson);
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
                    case 8:
                        documentXMLDocument.AddCollection();
                        break;
                    case 9:
                        responsiblePersonXMLDocument.AddCollection();
                        break;
                    case 10:
                        assetDocumentXMLDocument.AddCollection();
                        break;
                    case 11:
                        Console.Clear();
                        xmlDocument.ReturnAllAssets();
                        break;
                    case 12:
                        Console.Clear();
                        departmentXML.ReturnAllDepartments();
                        break;
                    case 13:
                        Console.Clear();
                        documentXMLDocument.ReturnAllDocuments();
                        break;
                    case 14:
                        Console.Clear();
                        responsiblePersonXMLDocument.ReturnAllResponsiblePersons();
                        break;
                    case 15:
                        Console.Clear();
                        assetDocumentXMLDocument.ReturnAllAssetDocuments();
                        break;
                }

                QueryMethods queryMethods = new QueryMethods();
                if (input == 16)
                {
                    Console.Clear();
                    Console.WriteLine("Оберіть запит який хочете виконати: \n" +
                        "\n1 - Отримати список вiдповiдальних персон id, яких бiльше 3, але менше 8" +
                        "\n2 - Отримати засоби, якi належать вiддiлу \"Виробничий пiдроздiл\"" +
                        "\n3 - Групувати засоби за вiддiлами" +
                        "\n4 - Вивести основнi засоби за документами у порядку зростання" +
                        "\n5 - Вивести документи, якi є у основного засоба \"Комп'ютер офiсний\"" +
                        "\n6 - Вивести вiддiли, якi починаються на букву \"А\" i вiдсортувати їх у порядку зростання" +
                        "\n7 - Отримати основнi засоби з найвищою та найнижчою цiною" +
                        "\n8 - Вивести основнi засоби, якi належать Андрiю Бондаренку" +
                        "\n9 - Згрупувати вiддiли, за кiлькiстю основних засобiв, якi їм належать" +
                        "\n10 - Отримати документи основного засобу Принтер лазерний  \r\n якi були зробленi в перiод " +
                                "вiд 1 серпня 2023 рокi по 11 червня 2024 року" +
                        "\n11 - Отримати пiдроздiли, у якому обслуговуються  \r\nосновнi засоби з iнвентарним номером 1301 або 1204" +
                        "\n12 - Отримати прiзвище та номер телефону вiдповiдальної особи  \r\nза основний засiб у якого id бiльше 5, але менше 9" +
                        "\n13 - Отримати загальну цiну всiх основних засобiв" +
                        "\n14 - Вiдсортувати основнi засоби за цiною" +
                        "\n15 - Отримати загальну кiлькiсть основних засобiв");

                    int inputQuery = Convert.ToInt32(Console.ReadLine());
                    switch(inputQuery) 
                    {
                        case 1:
                            Console.Clear();
                            queryMethods.GetResponsiblePersonsById();
                            break;
                        case 2:
                            Console.Clear();
                            queryMethods.GetAssetsByProducedDepartment();
                            break;
                        case 3: 
                            Console.Clear();
                            queryMethods.GroupAssetsByDepartments();
                            break;
                        case 4:
                            Console.Clear();
                            queryMethods.GetAssetsByDocuments();
                            break;
                        case 5:
                            Console.Clear();
                            queryMethods.GetDocumentsByAsset();
                            break;
                        case 6:
                            Console.Clear();
                            queryMethods.GetDepartmetsStartWithA();
                            break;
                        case 7:
                            Console.Clear();
                            queryMethods.GetAssetsWithMaxAndMinCost();
                            break;
                        case 8:
                            Console.Clear();
                            queryMethods.GetAssetsByResponsiblePerson();
                            break;
                        case 9:
                            Console.Clear();
                            queryMethods.GroupDepartmentsByAssetCount();
                            break;
                        case 10:
                            Console.Clear();
                            queryMethods.GetDocumentsByAssetAndDate();
                            break;
                        case 11:
                            Console.Clear();
                            queryMethods.GetDepartmentByAssetAndInventaryNum();
                            break;
                        case 12:
                            Console.Clear();
                            queryMethods.GetResponsiblePersonByAssetID();
                            break;
                        case 13:
                            Console.Clear();
                            queryMethods.GetTotalCostByAsset();
                            break;
                        case 14:
                            Console.Clear();
                            queryMethods.GetAssetsSortByCost();
                            break;
                        case 15:
                            Console.Clear();
                            queryMethods.GetAssetTotalCount();
                            break;
                    }
                }
                Console.WriteLine("Бажаєте продовжити? (+/-)");
                answer = Console.ReadLine();

            }
            while (answer == "+");

            asset = Asset.CreateAsset();
            xmlDocument.CreateXMLDocument(asset);
        }
    }
}
