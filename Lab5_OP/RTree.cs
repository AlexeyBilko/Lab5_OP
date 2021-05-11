using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    public class RTree
    {
        public int Size = 0;
        private int Capacity = 20;

        List<Place> list;

        public double latitudeMax { get; private set; }
        public double latitudeMin { get; private set; }
        public double longitudeMax { get; private set; }
        public double longitudeMin { get; private set; }

        public bool IsParent = false;
        public RTree FirstChild;
        public RTree SecondChild;

        public void Add(Place place)
        {
            Size++;
            if (Size == 1)
            {
                list = new List<Place>();
                latitudeMax = latitudeMin;
                latitudeMin = place.Latitude;

                longitudeMax = longitudeMin;
                longitudeMin = place.Longitude;

                list.Add(place);
            }

            else
            {
                latitudeMax = GetBigger(place.Latitude, latitudeMax);
                longitudeMax = GetBigger(place.Longitude, longitudeMax);
                latitudeMin = GetLower(place.Latitude, latitudeMin);
                longitudeMin = GetLower(place.Longitude, longitudeMin);

                if (IsParent)
                    InsertToChild(ref FirstChild, ref SecondChild, place);
                else
                {
                    list.Add(place);
                    if (Size > Capacity)
                        CreateChildren();
                }
            }
        }

        private static int CompareByLatitude(Place fisrtPlace, Place SecondPlace)
        {
            if (fisrtPlace.Latitude > SecondPlace.Latitude)
                return 1;
            if (fisrtPlace.Latitude < SecondPlace.Latitude)
                return -1;
            if (fisrtPlace.Longitude > SecondPlace.Longitude)
                return 1;
            if (fisrtPlace.Longitude < SecondPlace.Longitude)
                return -1;
            return 1;
        }

        private static int CompareByLongitude(Place firstPlace, Place SecondPlace)
        {
            if (firstPlace.Longitude > SecondPlace.Longitude)
                return 1;
            if (firstPlace.Longitude < SecondPlace.Longitude)
                return -1;
            if (firstPlace.Latitude > SecondPlace.Latitude)
                return 1;
            if (firstPlace.Latitude < SecondPlace.Latitude)
                return -1;
            return 1;
        }

        private void CreateChildren()
        {
            IsParent = true;

            FirstChild = new RTree();
            SecondChild = new RTree();

            if (latitudeMax - latitudeMin > longitudeMax - longitudeMin)
                list.Sort(CompareByLatitude);
            else list.Sort(CompareByLongitude);

            SecondChild.Add(list[Size - 1]);
            FirstChild.Add(list[0]);

            list.RemoveAt(Size - 1);
            list.RemoveAt(0);

            foreach (var item in list)
            {
                InsertToChild(ref FirstChild, ref SecondChild, item);
            }
        }

        public static double GetBigger(double first, double second)
        {
            if (first > second)
                return first;
            else return second;
        }
        public static double GetLower(double first, double second)
        {
            if (first < second)
                return first;
            else return second;
        }

        public static void InsertToChild(ref RTree FirstTree, ref RTree SecondTree, Place place)
        {
            double newLatitudeMax = GetBigger(FirstTree.latitudeMax, place.Latitude);
            double newLatitudeMin = GetLower(FirstTree.latitudeMax, place.Latitude);
            double newLongitudeMax = GetBigger(FirstTree.longitudeMax, place.Longitude);
            double newLongitudeMin = GetLower(FirstTree.longitudeMax, place.Longitude);

            double tmp1 = (newLatitudeMax - newLatitudeMin) * (newLongitudeMax - newLongitudeMin) + (SecondTree.latitudeMax - SecondTree.latitudeMin) * (SecondTree.longitudeMax - SecondTree.longitudeMin);

            newLatitudeMax = GetBigger(SecondTree.latitudeMax, place.Latitude);
            newLatitudeMin = GetLower(SecondTree.latitudeMax, place.Latitude);
            newLongitudeMax = GetBigger(SecondTree.longitudeMax, place.Longitude);
            newLongitudeMin = GetLower(SecondTree.longitudeMax, place.Longitude);

            double tmp2 = (FirstTree.latitudeMax - FirstTree.latitudeMin) * (FirstTree.longitudeMax - FirstTree.longitudeMin) + (newLatitudeMax - newLatitudeMin) * (newLongitudeMax - newLongitudeMin);

            if (tmp1 < tmp2)
                FirstTree.Add(place);
            else if (tmp2 > tmp1)
                SecondTree.Add(place);
            else
                FirstTree.Add(place);
        }
    }
}
