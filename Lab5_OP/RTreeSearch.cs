using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    class RTreeSearch
    {
        public static (Place, double) FindNearestPlaceBySubtype(RTree node, double x, double y)
        {
            int radius = 10;
            List<Place> entities = FindNearestEntities(node, x, y, radius);
            while ((entities == null || entities.Count == 0) && radius < 1000)
            {
                radius *= 2;
                entities = FindNearestEntities(node, x, y, radius);

            }
            if (entities == null) return (null, -1);
            return FindNearestOfTheList(entities, x, y);
        }
        public static (Place, double) FindNearestOfTheList(List<Place> entites, double x, double y)
        {
            Place nearest = null;
            double minimalLength = 1000;
            foreach (var Place in entites)
            {
                if (FindDistanceToPlace(Place, x, y) < minimalLength)
                {
                    nearest = Place;
                    minimalLength = FindDistanceToPlace(Place, x, y);
                }
            }
            return (nearest, minimalLength);
        }
        public static List<Place> FindNearestEntities(RTree node, double x, double y, int radius)
        {
            List<Place> entities = new List<Place>();
            if (node.IsParent)
            {
                var first = (IsRadiusIsEnough(node.FirstChild, x, y, radius) ? FindNearestEntities(node.FirstChild, x, y, radius) : null);
                var second = (IsRadiusIsEnough(node.SecondChild, x, y, radius) ? FindNearestEntities(node.SecondChild, x, y, radius) : null);
              
                
                if (first == null && second == null) return null;
                if (first == null) return second;
                if (second == null) return first;
                second.ForEach(item => first.Add(item));
                return first;
            }
            foreach (var Place in node.list)
            {
                if (FindDistanceToPlace(Place, x, y) <= radius)
                {
                    entities.Add(Place);
                }
            }
            return entities;
        }
        static bool IsRadiusIsEnough(RTree node, double x, double y, int radius)
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
        static double FindDistanceToPlace(Place Place, double x, double y)
        {
            double xCoef1 = 111 * Math.Cos(y);
            double xCoef2 = 111 * Math.Cos(Place.Longitude);
            double yCoef = 111;
            double xLength = ((Place.Latitude - x) * xCoef1 + (Place.Latitude - x) * xCoef2) / 2;
            double yLength = (Place.Longitude - y) * yCoef;
            return Math.Sqrt(Math.Pow(xLength, 2) + Math.Pow(yLength, 2));
        }
      
    }
}