using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public override void Draw()
        {
            ammo.Draw();
        }

        public override RectangleF GetCollider()
        {
            return ammo.GetCollider();
        }

        public override int GetDistance()
        {
            return ammo.GetDistance();
        }

        public override int GetRadius()
        {
            return ammo.GetRadius();
        }

        public override Vector2 GetSpeed()
        {
            Vector2 newSpeed = ammo.GetSpeed();
            newSpeed.X *= 1.5f;

            return newSpeed;
        }

        public override void Update()
        {
            ammo.Update();
        }
    }
}
