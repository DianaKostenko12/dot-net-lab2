using Lab2_.Net_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2_.Net_
{
    public class QueryMethods
    {
        private readonly string _personPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\ResponsiblePerson.xml";
        private readonly string _assetPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Asset.xml";
        private readonly string _departmentPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Department.xml";
        private readonly string _documentPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\Document.xml";
        private readonly string _assetDocumentPath = "D:\\University\\.Net\\Lab2\\Lab2(.Net)\\Lab2(.Net)\\XML Documents\\AssetDocument.xml";

        public void GetResponsiblePersonsById()
        {
            XDocument xdoc = XDocument.Load(_personPath);

            var q1 = from person in xdoc.Descendants("ResponsiblePerson")
                     where (int)person.Element("Id") > 3 && (int)person.Element("Id") < 8
                     select person;

            Console.WriteLine("\nСписок вiдповiдальних персон id, яких бiльше 3, але менше 8 ");

            if (q1.Any())
            {
                foreach (var person in q1)
                {
                    Console.WriteLine($"Id: {person.Element("Id").Value}, Прiзвище людини: {person.Element("Surname").Value}," +
                        $" Iм'я: {person.Element("Name").Value}");
                }
            }
            else
            {
                Console.WriteLine("Немає вiдповiдальних персон з такими умовами");
            }
        }

        public void GetAssetsByProducedDepartment()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument departmentsDoc = XDocument.Load(_departmentPath);

            var q2 = from asset in assetsdoc.Descendants("Asset")
                     join department in departmentsDoc.Descendants("Department") on (int)asset.Element("DepartmentId") equals (int)department.Element("Id")
                     where (string)department.Element("DepartmentName") == "Виробничий пiдроздiл"
                     select new { Name = (string)asset.Element("Name"), DepartmentName = (string)department.Element("DepartmentName") };

            Console.WriteLine("Засоби, якi належать вiддiлу \"Виробничий пiдроздiл\"");

            if (q2.Any())
            {
                foreach (var asset in q2)
                {
                    Console.WriteLine($"Засiб: {asset.Name} - Вiддiл: {asset.DepartmentName}");
                }
            }
            else
            {
                Console.WriteLine("Немає засобiв, якi належать вiддiлу \"Виробничий пiдроздiл\"");
            }
        }

        public void GroupAssetsByDepartments()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument departmentsDoc = XDocument.Load(_departmentPath);

            var q3 = from asset in assetsdoc.Descendants("Asset")
                     join department in departmentsDoc.Descendants("Department") on (int)asset.Element("DepartmentId") equals (int)department.Element("Id")
                     group asset by department into g
                     select new
                     {
                         Department = g.Key.Element("DepartmentName").Value,
                         Assets = g.ToList()
                     };

            Console.WriteLine($"\nГрупування засобiв за вiддiлами");
            if (q3.Any())
            {
                foreach (var group in q3)
                {
                    Console.WriteLine($"Department: {group.Department}");
                    foreach (var asset in group.Assets)
                    {
                        Console.WriteLine($"Asset Name: {asset.Element("Name").Value}," +
                            $" Inventory Number: {asset.Element("InventoryNumber").Value}, Initial Cost: {asset.Element("InitialCost").Value}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Немає даних для групування за вiддiлами");
            }
        }

        public void GetAssetsByDocuments()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument documentDoc = XDocument.Load(_documentPath);
            XDocument assetDocumentdoc = XDocument.Load(_assetDocumentPath);

            var q4 = from asset in assetsdoc.Descendants("Asset")
                     join assetDocument in assetDocumentdoc.Descendants("AssetDocument") on (int)asset.Element("Id") 
                     equals (int)assetDocument.Element("AssetId")
                     join document in documentDoc.Descendants("Document") on (int)assetDocument.Element("DocumentId") equals (int)document.Element("Id")
                     orderby (string)asset.Element("Name")
                     select new { AssetName = (string)asset.Element("Name"), DocumentName = (string)document.Element("DocumentName"), Date = (DateTime)assetDocument.Element("Date") };
            Console.WriteLine("Вивести основнi засоби за документами у порядку зростання");
            if (q4.Any())
            {
                foreach (var asset in q4)
                {
                    Console.WriteLine($"Засiб: {asset.AssetName} - {asset.DocumentName} Дата органiзацiї: {asset.Date}");
                }
            }
            else
            {
                Console.WriteLine("Немає основних засобiв за документами");
            }
        }

        public void GetDocumentsByAsset()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument documentDoc = XDocument.Load(_documentPath);
            XDocument assetDocumentdoc = XDocument.Load(_assetDocumentPath);

            var q5 = assetsdoc.Descendants("Asset")
                .Join(assetDocumentdoc.Descendants("AssetDocument"),
                    asset => (int)asset.Element("Id"),
                    assetDocument => (int)assetDocument.Element("AssetId"),
                    (asset, assetDocument) => new { Asset = asset, AssetDocument = assetDocument })
                .Join(documentDoc.Descendants("Document"),
                    combined => (int)combined.AssetDocument.Element("DocumentId"),
                    document => (int)document.Element("Id"),
                    (combined, document) => new
                    {
                        AssetName = (string)combined.Asset.Element("Name"),
                        DocumentName = (string)document.Element("DocumentName")
                    })
                .Where(combined => combined.AssetName == "Комп'ютер офiсний")
                .Select(combined => new { combined.AssetName, combined.DocumentName });

            Console.WriteLine("\nВивести документи, якi є у основного засоба \"Комп'ютер офiсний\"");
            if (q5.Any())
            {
                foreach (var asset in q5)
                {
                    Console.WriteLine($"Засiб: {asset.AssetName} - {asset.DocumentName}");
                }
            }
            else
            {
                Console.WriteLine("Немає документiв для основного засобу \"Комп'ютер офiсний\"");
            }
        }

        public void GetDepartmetsStartWithA()
        {
            XDocument departmentsDoc = XDocument.Load(_departmentPath);

            Console.WriteLine("\nВивести вiддiли, якi починаються на букву \"А\" i вiдсортувати їх у порядку зростання");

            var q6 = departmentsDoc.Descendants("Department")
                .Where(department => ((string)department.Element("DepartmentName")).StartsWith("А"))
                .OrderBy(department => (string)department.Element("DepartmentName"))
                .Select(department => new {
                    Id = (int)department.Element("Id"),
                    DepartmentName = (string)department.Element("DepartmentName")
                });
            if (q6.Any())
            {
                foreach (var department in q6)
                {
                    Console.WriteLine($"Вiддiл: {department.Id} - {department.DepartmentName}");
                }
            }
            else
            {
                Console.WriteLine("Немає вiддiлiв, якi починаються на букву \"А\"");
            }
        }

        public void GetAssetsWithMaxAndMinCost()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);

            Console.WriteLine("\nОтримати основнi засоби з найвищою та найнижчою цiною");
            decimal highestCost = assetsdoc.Descendants("Asset").Max(asset => (decimal)asset.Element("InitialCost"));
            decimal lowestCost = assetsdoc.Descendants("Asset").Min(asset => (decimal)asset.Element("InitialCost"));

            var assetHighestCost = assetsdoc.Descendants("Asset")
                .Where(asset => (decimal)asset.Element("InitialCost") == highestCost)
                .Select(asset => new {
                    InventoryNumber = (string)asset.Element("InventoryNumber"),
                    Name = (string)asset.Element("Name"),
                    InitialCost = (decimal)asset.Element("InitialCost")
                });

            var assetLowestCost = assetsdoc.Descendants("Asset")
                .Where(asset => (decimal)asset.Element("InitialCost") == lowestCost)
                .Select(asset => new {
                    InventoryNumber = (string)asset.Element("InventoryNumber"),
                    Name = (string)asset.Element("Name"),
                    InitialCost = (decimal)asset.Element("InitialCost")
                });

            var q7 = assetHighestCost.Concat(assetLowestCost);
            if (q7.Any())
            {
                foreach (var asset in q7)
                {
                    Console.WriteLine($"Засiб: {asset.InventoryNumber} {asset.Name} - {asset.InitialCost} грн");
                }
            }
            else
            {
                Console.WriteLine("Немає даних про основнi засоби з найвищою та найнижчою цiною");
            }
        }
        public void GetAssetsByResponsiblePerson()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument personDoc = XDocument.Load(_personPath);

            var q8 = personDoc.Descendants("ResponsiblePerson")
                    .Where(person => (string)person.Element("Name") == "Андрiй" && (string)person.Element("Surname") == "Бондаренко")
                    .Join(assetsdoc.Descendants("Asset"),
                    responsiblePerson => (int)responsiblePerson.Element("Id"),
                    asset => (int)asset.Element("ResponsiblePersonId"),
                    (responsiblePerson, asset) => new
                    {
                        AssetName = (string)asset.Element("Name"),
                        ResponsiblePersonSurname = (string)responsiblePerson.Element("Surname"),
                        ResponsiblePersonName = (string)responsiblePerson.Element("Name")
                    });
            Console.WriteLine("\nВивести основнi засоби, якi належать Андрiю Бондаренку");
            if (q8.Any())
            {
                foreach (var asset in q8)
                {
                    Console.WriteLine($"Вiдповiдальна персона: {asset.ResponsiblePersonSurname} " +
                        $"{asset.ResponsiblePersonName} - {asset.AssetName}");
                }
            }
            else
            {
                Console.WriteLine("Немає основних засобiв, якi належать Андрiю Бондаренку");
            }
        }

        public void GroupDepartmentsByAssetCount()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument departmentsDoc = XDocument.Load(_departmentPath);

            var q9 = from department in departmentsDoc.Descendants("Department")
                      join asset in assetsdoc.Descendants("Asset") on (int)department.Element("Id") equals (int)asset.Element("DepartmentId")
                      into assetsGroup
                      select new
                      {
                          Department = department.Element("DepartmentName").Value,
                          Count = assetsGroup.Count()
                      };
            Console.WriteLine("\nЗгрупувати вiддiли, за кiлькiстю основних засобiв, якi їм належать");
            if (q9.Any())
            {
                foreach (var group in q9)
                {
                    Console.WriteLine($"Вiддiл: {group.Department} - {group.Count}");
                }
            }
            else
            {
                Console.WriteLine("Немає даних для групування за кiлькiстю основних засобiв");
            }
        }

        public void GetDocumentsByAssetAndDate()
        {
            XDocument assetsdoc = XDocument.Load(_assetPath);
            XDocument documentDoc = XDocument.Load(_documentPath);
            XDocument assetDocumentdoc = XDocument.Load(_assetDocumentPath);

            var q10 = from asset in assetsdoc.Descendants("Asset")
                      join assetDocument in assetDocumentdoc.Descendants("AssetDocument") on (int)asset.Element("Id") equals (int)assetDocument.Element("AssetId")
                      join document in documentDoc.Descendants("Document") on (int)assetDocument.Element("DocumentId") equals (int)document.Element("Id")
                      where (string)asset.Element("Name") == "Принтер лазерний"
                   && (DateTime)assetDocument.Element("Date") >= new DateTime(2023, 8, 1)
                   && (DateTime)assetDocument.Element("Date") <= new DateTime(2024, 6, 11)
                      select new
                      {
                          AssetName = (string)asset.Element("Name"),
                          DocumentName = (string)document.Element("DocumentName"),
                          DocumentDate = (DateTime)assetDocument.Element("Date")
                      };
            Console.WriteLine("\nОтримати документи основного засобу Принтер лазерний " +
                "i\n якi були зробленi в перiод вiд 1 серпня 2023 року по 11 червня 2024 року");

            if (q10.Any())
            {
                foreach (var document in q10)
                {
                    Console.WriteLine($"{document.AssetName}: {document.DocumentName} {document.DocumentDate}");
                }
            }
            else
            {
                Console.WriteLine("Немає документiв для основного засобу Принтер лазерний у заданому перiодi");
            }
        }

        public void GetDepartmentByAssetAndInventaryNum()
        {
            XDocument assetsDoc = XDocument.Load(_assetPath);
            XDocument departmentsDoc = XDocument.Load(_departmentPath);

            var q11 = assetsDoc.Descendants("Asset")
                      .Join(departmentsDoc.Descendants("Department"),
                            asset => (int)asset.Element("DepartmentId"),
                            department => (int)department.Element("Id"),
                            (asset, department) => new
                            {
                                AssetNum = (string)asset.Element("InventoryNumber"),
                                AssetName = (string)asset.Element("Name"),
                                Department = (string)department.Element("DepartmentName")
                            })
                      .Where(combined => combined.AssetNum == "1301" || combined.AssetNum == "1204");
            Console.WriteLine("\n Отримати пiдроздiли, у якому обслуговуються " +
                "основнi засоби з iнвентарним номером 1301 або 1204");
            if (q11.Any())
            {
                foreach (var department in q11)
                {
                    Console.WriteLine($" {department.AssetNum} {department.AssetName} - {department.Department}");
                }
            }
            else
            {
                Console.WriteLine("Немає даних для основних засобiв з iнвентарним номером 1301 або 1204");
            }
        }

        public void GetResponsiblePersonByAssetID()
        {
            XDocument assetsDoc = XDocument.Load(_assetPath);
            XDocument personDoc = XDocument.Load(_personPath);

            var q12 = from asset in assetsDoc.Descendants("Asset")
                      join responsiblePerson in personDoc.Descendants("ResponsiblePerson")
                      on (int)asset.Element("ResponsiblePersonId") equals (int)responsiblePerson.Element("Id")
                      where (int)asset.Element("Id") >= 5 && (int)asset.Element("Id") <= 9
                      select new
                      {
                          AssetId = (int)asset.Element("Id"),
                          AssetName = (string)asset.Element("Name"),
                          Surname = (string)responsiblePerson.Element("Surname"),
                          Phone = (string)responsiblePerson.Element("Phone")
                      };
            Console.WriteLine("\n Отримати прiзвище та номер телефону вiдповiдальної особи " +
                "за основний засiб у якого id бiльше 5, але менше 9");
            if (q12.Any())
            {
                foreach (var responsiblePerson in q12)
                {
                    Console.WriteLine($" {responsiblePerson.AssetId} {responsiblePerson.AssetName} - вiдповiдальна особа {responsiblePerson.Surname} {responsiblePerson.Phone}");
                }
            }
            else
            {
                Console.WriteLine("Немає вiдповiдальної особи за основнi засоби з id бiльше 5, але менше 9");
            }
        }

        public void GetTotalCostByAsset()
        {
            XDocument assetsDoc = XDocument.Load(_assetPath);
            var q13 = assetsDoc.Descendants("Asset").Sum(asset => (int)asset.Element("InitialCost"));
            Console.WriteLine("\nОтримати загальну цiну всiх основних засобiв" + " - " + q13 + "грн");
        }

        public void GetAssetsSortByCost()
        {
            XDocument assetsDoc = XDocument.Load(_assetPath);

            var q14 = from asset in assetsDoc.Descendants("Asset")
                      orderby (int)asset.Element("InitialCost")
                      select asset;
            Console.WriteLine("\nВiдсортувати основнi засоби за цiною");
            if (q14.Any())
            {
                foreach (var asset in q14)
                {
                    Console.WriteLine($"{(int)asset.Element("Id")} {(string)asset.Element("InventoryNumber")} {(string)asset.Element("Name")} {(int)asset.Element("InitialCost")}");
                }
            }
            else
            {
                Console.WriteLine("Немає даних для сортування за цiною");
            }
        }
        public void GetAssetTotalCount()
        {
            XDocument assetsDoc = XDocument.Load(_assetPath);
            var q15 = assetsDoc.Descendants("Asset").Count(asset => asset.Element("Id") != null);
            Console.WriteLine($"\nОтримати загальну кiлькiсть основних засобiв {q15}");
        }

    }
}
