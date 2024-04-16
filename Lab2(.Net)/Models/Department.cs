using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_.Net_.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public Department(int id, string departmentName)
        {
            Id = id;
            DepartmentName = departmentName;
        }

        public Department()
        {
            
        }

        public static Department CreateDepartment()
        {
            int departmentId;
            string departmentName;
            do
            {
                Console.WriteLine("Введiть ID вiддiлу:");
                if (!int.TryParse(Console.ReadLine(), out departmentId) || departmentId <= 0)
                {
                    Console.WriteLine("Неправильний ID вiддiлу. Має бути додатним цiлим числом.");
                    continue;
                }

                Console.WriteLine("Ведiть назву вiддiлу:");
                departmentName = Console.ReadLine();
                break;
            } while (true);

            return new Department(departmentId, departmentName);
        }
    }
}
