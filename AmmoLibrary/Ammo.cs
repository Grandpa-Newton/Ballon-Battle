using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public abstract class Ammo
    {
        public abstract double Speed { get; set; }
        public abstract int Distancde { get; set; }
        public abstract int Radius { get; set; }
    }
}
