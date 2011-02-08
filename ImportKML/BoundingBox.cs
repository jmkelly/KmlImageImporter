using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportKml.Core
{
    public class BoundingBox
    {
        public BoundingBox(Coordinate ne, Coordinate sw)
        {
            this.NorthEast = ne;
            this.SouthWest = sw;

        }

        public Coordinate NorthEast { get; set; }
        public Coordinate SouthWest { get; set; }
    }
}
