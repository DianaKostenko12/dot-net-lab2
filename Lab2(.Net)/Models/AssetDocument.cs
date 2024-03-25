using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_.Net_.Models
{
    public class AssetDocument
    {
        public int AssetId { get; set; }
        public int DocumentId { get; set; }
        public DateTime Date { get; set; }

        public AssetDocument(int assetId, int documentId, DateTime date)
        {
            AssetId = assetId;
            DocumentId = documentId;
            Date = date;
        }

        public AssetDocument()
        {
            
        }

        public static AssetDocument CreateAssetDocument()
        {
            int assetId, documentId, year, month, day;
            DateTime date;
            do
            {
                Console.WriteLine("Введiть ID основного засобу:");
                if (!int.TryParse(Console.ReadLine(), out assetId) || assetId <= 0)
                {
                    Console.WriteLine("Неправильний ID основного засобу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Введiть ID документу:");
                if (!int.TryParse(Console.ReadLine(), out documentId) || documentId <= 0)
                {
                    Console.WriteLine("Неправильний ID документу. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Введiть рік проведення операції:");
                if (!int.TryParse(Console.ReadLine(), out year) || year <= 0)
                {
                    Console.WriteLine("Неправильно введений рік. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Введiть місяць проведення операції:");
                if (!int.TryParse(Console.ReadLine(), out month) || month <= 0)
                {
                    Console.WriteLine("Неправильно введений місяць. Має бути додатним цілим числом.");
                    continue;
                }

                Console.WriteLine("Введiть день проведення операції:");
                if (!int.TryParse(Console.ReadLine(), out day) || day <= 0)
                {
                    Console.WriteLine("Неправильно введений день. Має бути додатним цілим числом.");
                    continue;
                }
                
                date = new DateTime(year, month, day);

                break;
            } while (true);

            return new AssetDocument(assetId, documentId, date);
        }
    }
}
