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
            string FileName, Latitude, Longitude, count, type;

            Console.WriteLine("coordinates and size!");

            string stroka = Console.ReadLine();

            string[]items = stroka.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);

            FileName = items[0];
            Latitude = items[1];
            Longitude = items[2];
            count = items[3];
            type = items[4];


            RNode rTree = FillRTree(FileName);

            Console.WriteLine();

            List<Place> nearest = RNodeSearch.FindNearestPlacesByCount(rTree, double.Parse(Latitude), double.Parse(Longitude), int.Parse(count), type);

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
                    Console.WriteLine($" {index} title: no title | type: {item.Type}");
                    index++;
                }
                else {
                    Console.WriteLine($" {index} title: {item.Title} | type: {item.Type}");
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
