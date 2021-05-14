using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName, Latitude, Longitude, size;

            Console.WriteLine("coordinates and size!");

            string stroka = Console.ReadLine();

            string[]items = stroka.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);

            FileName = items[0];
            Latitude = items[1];
            Longitude = items[2];
            size = items[3];


            RNode rTree = FillRTree(FileName);

            Console.WriteLine();

            List<Place> nearest = RTreeSearch.FindNearestPlaces(rTree, double.Parse(Latitude), double.Parse(Longitude), int.Parse(size));

            Output(nearest);
            
            Console.WriteLine();
        }

        private static void Output(List<Place> nearest)
        {
            int index = 1;
            Console.WriteLine("Title => Adress");
            foreach (var item in nearest)
            {
                if (item.Title != "")
                {
                    Console.WriteLine($" {index} title: { item.Title}");
                    index++;
                }
            }
        }

        public static RNode FillRTree(string filename)
        {
            RNode tree = new RNode();
            List<string> list = new List<string>();

            list = File.ReadAllLines(filename).ToList();
            foreach (var item in list)
            {
                string[] buf = item.Split(';');
                tree.Add(new Place(double.Parse(buf[0]), double.Parse(buf[1]), buf[2], buf[3], buf[4], buf[5]));
            }

            return tree;
        }

        //ukraine_poi.csv 48 35 15

    }
}
