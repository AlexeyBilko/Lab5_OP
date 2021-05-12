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



        }

        public static RTree FillRTree(string filename)
        {
            RTree tree = new RTree();
            List<string> list = new List<string>();
            File.AppendAllLines(filename, list);

            foreach (var item in list)
            {
                string[] buf = item.Split(';');
                tree.Add(new Place(double.Parse(buf[0]), double.Parse(buf[1]), buf[2], buf[3], buf[4], buf[5]));
            }

            return tree;
        }
    }

   


}
