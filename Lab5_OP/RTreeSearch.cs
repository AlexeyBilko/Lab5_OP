using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    class RTreeSearch
    {
        public static List<Place> FindPlaces(RTree node, double x, double y, int radius, string type, string subtype)
        {
            List<Place> places = new List<Place>();
            if (node.IsParent)
            {
                var first = (CheckRadius(node.FirstChild, x, y, radius) ? FindPlaces(node.FirstChild, x, y, radius, type, subtype) : null);
                var second = (CheckRadius(node.SecondChild, x, y, radius) ? FindPlaces(node.SecondChild, x, y, radius, type, subtype) : null);
                if (first == null && second == null) return null;
                if (first == null) return second;
                if (second == null) return first;
                second.ForEach(item => first.Add(item));
                return first;
            }
            foreach (var site in node.list)
            {
                if (DistanceToPlace(site, x, y) <= radius && HaveTheSameType(site, type, subtype))
                {
                    places.Add(site);
                }
            }
            return places;
        }
        
    }
}