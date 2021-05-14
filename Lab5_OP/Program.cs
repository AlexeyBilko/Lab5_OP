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
            Console.WriteLine("Enter filename and coordinates and size!");

            string type = "all", subtype = "all";

            string stroka = Console.ReadLine();

            string[]items = stroka.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
            

            string filename = Console.ReadLine();
            RTree rTree = FillRTree(items[0]);

            Console.WriteLine();

            List<Place> nearest = RTreeSearch.FindNearestEntities(rTree, double.Parse(items[1]), double.Parse(items[2]), int.Parse(items[3]));

            foreach (var item in nearest)
            {
                Console.WriteLine(item.Address);
            }
            Console.WriteLine();

            /*foreach (var item in nearest)
            {
                Console.WriteLine(item.ne);
            }*/
        }

       

        public static RTree FillRTree(string filename)
        {
            RTree tree = new RTree();
            using (StreamReader sr = new StreamReader(filename))
            {
                for (int ctr = 0; !sr.EndOfStream; ctr++)
                {
                    string str = sr.ReadLine();
                    if (str == null) continue;
                    try
                    {
                        string[] items = str.Split(';');
                        if (items.Length < 6) continue;
                        tree.Add(new Place(double.Parse(items[0]), double.Parse(items[1]), items[2], items[3], items[4], items[5]));
                    }
                    catch (IndexOutOfRangeException) { Console.WriteLine($"Oops, damaged line {ctr} in file... Let`s skip it!"); }
                }
            }
            return tree;
        }
        // ukraine_poi.csv 50 30 300 
       
    }
}
