using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportKml.Core
{
    public class Coordinate
    {

        public Coordinate(double lon, double lat)
        {
            this.Lon = lon;
            this.Lat = lat;
        }

        public double Lon { get; set; }
        public double Lat { get; set; }

    }
}
