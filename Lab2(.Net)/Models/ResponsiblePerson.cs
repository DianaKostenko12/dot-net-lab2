using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_.Net_.Models
{
    public class ResponsiblePerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public ResponsiblePerson(int id, string name, string surname, string phone)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Phone = phone;
        }

        public static ResponsiblePerson CreateResponsiblePerson()
        {
            int id;
            string name, surname, phone;
            do
            {
                Console.WriteLine("Введiть ID відповідальної персони:");
                if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
                {
                    Console.WriteLine("Неправильний ID відповідальної персони. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Ведіть ім'я відповідальної персони:");
                name = Console.ReadLine();
                if (name == null)
                {
                    Console.WriteLine("Некоретно введене значення");
                    continue;
                }
                Console.WriteLine("Ведіть прізвище відповідальної персони:");
                surname = Console.ReadLine();
                if (surname == null)
                {
                    Console.WriteLine("Некоретно введене значення");
                    continue;
                }

                Console.WriteLine("Введіть номер телефону відповідальної персони");
                phone = Console.ReadLine();
                if (phone == null)
                {
                    Console.WriteLine("Некоретно введене значення");
                    continue;
                }

                break;
            } while (true);

            return new ResponsiblePerson(id, name, surname, phone);
        }
    }
}
