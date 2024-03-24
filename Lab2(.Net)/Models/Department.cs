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

        public static Department CreateDepartment()
        {
            int departmentId;
            string departmentName;
            do
            {
                Console.WriteLine("Введiть ID відділу:");
                if (!int.TryParse(Console.ReadLine(), out departmentId) || departmentId <= 0)
                {
                    Console.WriteLine("Неправильний ID відділу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Ведіть назву відділу:");
                departmentName = Console.ReadLine();
                break;
            } while (true);

            return new Department(departmentId, departmentName);
        }
    }
}
