using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    public class Place
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Type { get; set; }
        public string SubType { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public Place(double latitude, double longitude, string type, string subtype, string title, string address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Type = type;
            SubType = subtype;
            Title = title;
            Address = address;
        }
    }
}
