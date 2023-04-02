using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public abstract class Ammo
    {
        public abstract double GetSpeed();
        public abstract int GetDistance();
        public abstract int GetRadius();
    }
}
