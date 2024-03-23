using Lab2_.Net_.AddXMLDocument;
using Lab2_.Net_.Models;
using System;
using System.Xml;

namespace Lab2_.Net_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer = "";
            AssetXMLDocument xmlDocument = new AssetXMLDocument();
            Asset asset;
            do
            {
                Console.Clear();
                Console.WriteLine("Що будемо робити? \n" +
                    "1-Додати новий об'єкт до XML документу Asset?");

                int input = Convert.ToInt32(Console.ReadLine());
                if(input == 1)
                {
                    asset = Asset.CreateAsset();
                    xmlDocument.AddXMLElement(asset);
                }
                Console.WriteLine("Бажаєте продовжити? (+/-)");
                answer = Console.ReadLine();

            }
            while (answer == "+");
        }
    }
}
