using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary.characteristics_changing
{
    public class RadiusDecorator : AmmoDecorator
    {
        public RadiusDecorator(Ammo ammo) : base(ammo)
        {
        }

        public override int GetDistance()
        {
            return ammo.GetDistance();
        }

        public override int GetRadius()
        {
            return (int)(ammo.GetDistance() * 1.2);
        }

        public override double GetSpeed()
        {return ammo.GetSpeed();
        }
    }
}
