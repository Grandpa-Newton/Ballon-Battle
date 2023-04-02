using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    internal class PiercingAmmo : Ammo // бронебойный 
    {
        public override int GetDistance()
        {
            return 120;
        }

        public override int GetRadius()
        {
            return 45;
        }

        public override double GetSpeed()
        {
            return 3.0;
        }
    }
}
