using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    internal class ExplosiveAmmo : Ammo // фугасный
    {
        public override int GetDistance()
        {
            return 100;
        }

        public override int GetRadius()
        {
            return 50;
        }

        public override double GetSpeed()
        {
            return 5.0;
        }
    }
}
