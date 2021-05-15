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


            RTree rTree = FillRTree(FileName);

            Console.WriteLine();

            List<Place> nearest = RTreeSearch.FindNearestEntities(rTree, double.Parse(Latitude), double.Parse(Longitude), int.Parse(size));

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

        public static RTree FillRTree(string filename = "ukraine_poi.csv")
        {
            RTree tree = new RTree();
            using (StreamReader sr = new StreamReader(filename))
            {
                for (int ctr = 0; !sr.EndOfStream; ctr++)
                {
                    string str = sr.ReadLine();
                   
                    string[] items = str.Split(';');
                    if (items.Length < 6) continue;
                    tree.Add(new Place(double.Parse(items[0]), double.Parse(items[1]), items[2], items[3], items[4], items[5]));

                }
            }
            return tree;
        }
        // ukraine_poi.csv 50 30 300 
       
    }
}
