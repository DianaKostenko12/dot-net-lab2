using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_.Net_.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public Document(int id, string documentName)
        {
            Id = id;
            DocumentName = documentName;
        }

        public Document()
        {
            
        }

        public static Document CreateDocument()
        {
            int documentId;
            string documentName;
            do
            {
                Console.WriteLine("Введiть ID документу:");
                if (!int.TryParse(Console.ReadLine(), out documentId) || documentId <= 0)
                {
                    Console.WriteLine("Неправильний ID документу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Ведіть назву докумену:");
                documentName = Console.ReadLine();
                break;
            } while (true);

            return new Document(documentId, documentName);
        }
    }
}
