using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_OP
{
    class Place
    {
        double Latitude { get; set; }
        double Longitude { get; set; }

        string Type { get; set; }
        string SubType { get; set; }
        string Title { get; set; }
        string Address { get; set; }

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
