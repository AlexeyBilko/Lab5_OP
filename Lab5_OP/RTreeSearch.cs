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
        static bool CheckRadius(RTree node, double x, double y, int radius)
        {
            double nearestX = x < node.latitudeMin ? node.latitudeMin : x > node.latitudeMax ? node.latitudeMax : x;
            double nearestY = y < node.longitudeMin ? node.longitudeMin : x > node.longitudeMax ? node.longitudeMax : y;
            double xCoef1 = 111 * Math.Cos(y);
            double xCoef2 = 111 * Math.Cos(nearestY);
            double yCoef = 111;
            double xLength = ((nearestX - x) * xCoef1 + (nearestX - x) * xCoef2) / 2;
            double yLength = (nearestY - y) * yCoef;
            return Math.Sqrt(Math.Pow(xLength, 2) + Math.Pow(yLength, 2)) <= radius;
        }

        static double DistanceToPlace(Place site, double x, double y)
        {
            double xCoef1 = 111 * Math.Cos(y);
            double xCoef2 = 111 * Math.Cos(site.Longitude);
            double yCoef = 111;
            double xLength = ((site.Latitude - x) * xCoef1 + (site.Latitude - x) * xCoef2) / 2;
            double yLength = (site.Longitude - y) * yCoef;
            return Math.Sqrt(Math.Pow(xLength, 2) + Math.Pow(yLength, 2));
        }
        static bool HaveTheSameType(Place site, string type, string subtype)
        {
            return (type == "all" || type == site.Type) && (subtype == "all" || subtype == site.SubType);
        }
    }
}