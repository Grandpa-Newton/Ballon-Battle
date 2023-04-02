using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary.characteristics_changing
{
    public class DistanceDecorator : AmmoDecorator
    {
        public DistanceDecorator(Ammo ammo) : base(ammo)
        {
        }

        public override int GetDistance() // как в speed
        {
            return (int)(ammo.GetDistance()*1.2);
        }

        public override int GetRadius()
        {
            return ammo.GetRadius();
        }

        public override double GetSpeed()
        {
            return ammo.GetSpeed();
        }
    }
}
