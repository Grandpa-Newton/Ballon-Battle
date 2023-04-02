using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary.characteristics_changing
{
    public class SpeedDecorator : AmmoDecorator
    {
        public SpeedDecorator(Ammo ammo) : base(ammo)
        {
        }

        public override int GetDistance()
        {
            return ammo.GetDistance();
        }

        public override int GetRadius()
        {
            return ammo.GetRadius();
        }

        public override double GetSpeed()
        {
            return ammo.GetSpeed() * 1.5;
        }
    }
}
