using System;
using System.Collections.Generic;

namespace Lab5_OP
{
    class RNodeSearch
    {
        public static List<Place> FindNearestPlaces(RNode node, double x, double y, int radius, string type)
        {
            List<Place> Places = new List<Place>();
            if (node.IsParent)
            {
                var first = new List<Place>();
                if (IsRadiusIsEnough(node.FirstChild, x, y, radius))
                     first = FindNearestPlaces(node.FirstChild, x, y, radius, type);
                else
                    first = null;


                var second = new List<Place>();
                if (IsRadiusIsEnough(node.SecondChild, x, y, radius))
                    second = FindNearestPlaces(node.SecondChild, x, y, radius,type);
                else
                    second = null;


                if (first == null && second == null) 
                    return null;
                if (first == null) 
                    return second;
                if (second == null) 
                    return first;

                foreach (var item in second)
                    first.Add(item);

                return first;
            }
            foreach (var Place in node.list)
                if (FindDistanceToPlace(Place, x, y) <= radius && type.ToString() == Place.Type.ToString())
                    Places.Add(Place);
            
            return Places;
        }

        public static List<Place> FindNearestPlacesByCount(RNode node, double x, double y, int count, string type)
        {
            int radius = 5;
            List<Place> places = FindNearestPlaces(node, x, y, radius, type);
            try
            {
                while ((places.Count <= count - 1))
                {
                    radius *= 2;
                    places = FindNearestPlaces(node, x, y, radius, type);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Out of range");
            }

            places.RemoveRange(count, places.Count - count);

            return places;
        }

        static bool IsRadiusIsEnough(RNode node, double x, double y, int radius)
        {
            double nearestX;
            if (x < node.latitudeMin)
                nearestX = node.latitudeMin;
            if (x > node.latitudeMax)
                nearestX = node.latitudeMax;
            nearestX = x;


            double nearestY;
            if (y < node.longitudeMin)
                nearestY = node.longitudeMin;
            if (x > node.longitudeMax)
                nearestY = node.longitudeMax;
            nearestY = y;

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