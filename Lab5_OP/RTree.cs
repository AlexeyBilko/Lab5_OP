using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    public class RTree
    {
        public bool IsParent = false;
        public RTree FirstChild;
        public RTree SecondChild;

        public int Size = 0;
        private int capacity = 10;

        public double latitudeMax;
        public double latitudeMin;
        public double longitudeMax;
        public double longitudeMin;

        public List<Place> list = new List<Place>();

        public void Add(Place e)
        {
            Size++;

            if (Size == 1)
            {
                latitudeMax = latitudeMin = e.Latitude;
                longitudeMax = longitudeMin = e.Longitude;

                list.Add(e);
            }
            else
            {
                latitudeMax = FindBigger(e.Latitude, latitudeMax);
                longitudeMax = FindBigger(e.Longitude, longitudeMax);
                latitudeMin = FindLower(e.Latitude, latitudeMin);
                latitudeMax = FindLower(e.Latitude, latitudeMax);

                if (IsParent)
                {
                    if (OptimalInclude(FirstChild, SecondChild, e))
                        FirstChild.Add(e);
                    else SecondChild.Add(e);
                }
                else
                {
                    list.Add(e);
                    if (Size > capacity)
                        Divide();
                }
            }
        }

        public static double FindBigger(double first, double second)
        {
            if (first > second)
                return first;
            else return second;
        }
        public static double FindLower(double first, double second)
        {
            if (first < second)
                return first;
            else return second;
        }

        private static int CompareByCoordinateX(Place firstplace, Place secondplace)
        {
            if (firstplace.Latitude > secondplace.Latitude)
                return 1;
            if (firstplace.Latitude < secondplace.Latitude)
                return -1;
            if (firstplace.Longitude > secondplace.Longitude)
                return 1;
            if (firstplace.Longitude < secondplace.Longitude)
                return -1;
            return 1;
        }

        private static int CompareByCoordinateY(Place firstplace, Place secondplace)
        {
            if (firstplace.Longitude > secondplace.Longitude)
                return 1;
            if (firstplace.Longitude < secondplace.Longitude)
                return -1;
            if (firstplace.Latitude > secondplace.Latitude)
                return 1;
            if (firstplace.Latitude < secondplace.Latitude)
                return -1;
            return 1;
        }

        private void Divide()
        {
            IsParent = true;

            FirstChild = new RTree();
            SecondChild = new RTree();

            if (latitudeMax - latitudeMin > longitudeMax - longitudeMin)
                list.Sort(CompareByCoordinateX);
            else list.Sort(CompareByCoordinateY);

            FirstChild.Add(list[0]);
            SecondChild.Add(list[Size - 1]);

            list.RemoveAt(Size - 1);
            list.RemoveAt(0);

            foreach (Place Place in list)
            {
                if (OptimalInclude(FirstChild, SecondChild, Place)) FirstChild.Add(Place);
                else SecondChild.Add(Place);
            }
        }
        public static bool OptimalInclude(RTree first, RTree second, Place place)
        {
            double lalitudeMax1 = FindBigger(first.latitudeMax, place.Latitude);
            double latitudeXMin1 = FindLower(first.latitudeMax, place.Latitude);

            double longitudeMax1 = FindBigger(first.longitudeMax, place.Longitude);
            double longitudeMin1 = FindLower(first.longitudeMax, place.Longitude);

            double tmp1 = (lalitudeMax1 - latitudeXMin1) *
                          (longitudeMax1 - longitudeMin1) +
                          (second.latitudeMax - second.latitudeMin) *
                          (second.longitudeMax - second.longitudeMin);

            double latitudeMax2 = FindBigger(second.latitudeMax, place.Latitude);
            double latitudeMin2 = FindLower(second.latitudeMax, place.Latitude);

            double longitudeMax2 = FindBigger(second.longitudeMax, place.Longitude);
            double longitudeMin2 = FindLower(second.longitudeMax, place.Longitude);

            double tmp2 = (first.latitudeMax - first.latitudeMin) *
                          (first.longitudeMax - first.longitudeMin) +
                          (latitudeMax2 - latitudeMin2) *
                          (longitudeMax2 - longitudeMin2);

            if(tmp1 != tmp2)
                return tmp1 < tmp2;
            else return first.Size < second.Size;
        }
    }
}
