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
                    Console.WriteLine("Неправильний ID основного засобу. Має бути додатним цiлим числом.");
                    continue;
                }

                Console.WriteLine("Введiть iнвентарний номер основного засобу:");
                inventoryNumberStr = Console.ReadLine();
                if (!int.TryParse(inventoryNumberStr, out inventoryNumber) || inventoryNumber <= 0)
                {
                    Console.WriteLine("Неправильний iнвентарний номер основного засобу. Має бути додатним цiлим числом.");
                    continue;
                }

                Console.WriteLine("Ведiть назву основного засобу:");
                assetName = Console.ReadLine();
                if (assetName == null)
                {
                    Console.WriteLine("Некоретно введене значення");
                    continue;
                }

                Console.WriteLine("Введiть початкову вартiсть основного засобу:");
                if (!decimal.TryParse(Console.ReadLine(), out initialCost) || initialCost <= 0)
                {
                    Console.WriteLine("Неправильно введена первинна вартiсть. Має бути додатним числом.");
                    continue;
                }

                Console.WriteLine("Введiть ID вiддiлу:");
                if (!int.TryParse(Console.ReadLine(), out departmentId) || departmentId <= 0)
                {
                    Console.WriteLine("Неправильний ID вiддiлу. Має бути додатним цiлим числом.");
                    continue;
                }

                Console.WriteLine("Введiть ID вiдповiдальної особи:");
                if (!int.TryParse(Console.ReadLine(), out responsiblePersonId) || responsiblePersonId <= 0)
                {
                    Console.WriteLine("Неправильний ID вiдповiдальної особи. Має бути додатним цiлим числом.");
                    continue;
                }

                break;
            } while (true);

            return new Asset(assetId, inventoryNumberStr, assetName, initialCost, departmentId, responsiblePersonId);
        }
    }
}
