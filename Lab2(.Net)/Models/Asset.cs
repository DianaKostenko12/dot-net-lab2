using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_.Net_.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Name { get; set; }
        public decimal InitialCost { get; set; }
        public int DepartmentId { get; set; }
        public int ResponsiblePersonId { get; set; }

        public Asset(int id, string inventoryNumber, string name, decimal initialCost, int departmentId,
            int responsiblePersonId)
        {
            Id = id;
            InventoryNumber = inventoryNumber;
            Name = name;
            InitialCost = initialCost;
            DepartmentId = departmentId;
            ResponsiblePersonId = responsiblePersonId;
        }

        public Asset() { }
         
        public static Asset CreateAsset()
        {
            int assetId, inventoryNumber, departmentId, responsiblePersonId;
            string inventoryNumberStr, assetName;
            decimal initialCost;

            do
            {
                Console.WriteLine("Введiть ID основного засобу:");
                if (!int.TryParse(Console.ReadLine(), out assetId) || assetId <= 0)
                {
                    Console.WriteLine("Неправильний ID основного засобу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Введіть інвентарний номер основного засобу:");
                inventoryNumberStr = Console.ReadLine();
                if (!int.TryParse(inventoryNumberStr, out inventoryNumber) || inventoryNumber <= 0)
                {
                    Console.WriteLine("Неправильний інвентарний номер основного засобу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Ведіть назву основного засобу:");
                assetName = Console.ReadLine();
                if (assetName == null)
                {
                    Console.WriteLine("Некоретно введене значення");
                    continue;
                }

                Console.WriteLine("Введіть початкову вартість основного засобу:");
                if (!decimal.TryParse(Console.ReadLine(), out initialCost) || initialCost <= 0)
                {
                    Console.WriteLine("Неправильно введена первинна вартість. Має бути додатним числом.");
                    continue;
                }

                Console.WriteLine("Введiть ID відділу:");
                if (!int.TryParse(Console.ReadLine(), out departmentId) || departmentId <= 0)
                {
                    Console.WriteLine("Неправильний ID відділу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Введiть ID відповідальної особи:");
                if (!int.TryParse(Console.ReadLine(), out responsiblePersonId) || responsiblePersonId <= 0)
                {
                    Console.WriteLine("Неправильний ID відповідальної особи. Має бути додатним цілим числом.");
                    continue;
                }

                break;
            } while (true);

            return new Asset(assetId, inventoryNumberStr, assetName, initialCost, departmentId, responsiblePersonId);
        }
    }
}
