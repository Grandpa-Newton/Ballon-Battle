using OpenTK;
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

        public override void Draw()
        {
            ammo.Draw();
        }

        public override int GetDistance()
        {
            return ammo.GetDistance();
        }

        public override int GetRadius()
        {
            return (int)(ammo.GetDistance() * 1.2);
        }

        public override Vector2 GetSpeed()
        {
            return ammo.GetSpeed();
        }

        public override void Update()
        {
            ammo.Update();
        }
    }
}
