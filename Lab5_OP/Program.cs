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
            Console.WriteLine("Enter filename");
            string filename = Console.ReadLine();
            RTree rTree = FillRTree(filename);


            Console.ReadLine();
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
        /*
          public static RTree FillRTree(string filename)
        {
            RTree tree = new();
            using (StreamReader sr = new(filename))
            {
                for (int ctr = 0; !sr.EndOfStream; ctr++)
                {
                    string str = sr.ReadLine();
                    if (str == null) continue;
                    try
                    {
                        string[] items = str.Split(";");
                        if (items.Length < 6) continue;
                        tree.Add(new Entity(double.Parse(items[0]), double.Parse(items[1]), items[2], items[3], items[4], items[5]));
                    }
                    catch (IndexOutOfRangeException) { Console.WriteLine($"Oops, damaged line {ctr} in file... Let`s skip it!"); }
                }
            }
            return tree;
        }
   
         */

        /*
         public static RTree FillRTree(string path)
        {
            RTree tree = new RTree();
            List<string> list = new List<string>();
            File.AppendAllLines(path, list);

            foreach (var item in list)
            {
                string[] buf = item.Split(';');
                tree.Add(new Place(double.Parse(buf[0]), double.Parse(buf[1]), buf[2], buf[3], buf[4], buf[5]));
            }

            return tree;
        }
         */

    }
}
