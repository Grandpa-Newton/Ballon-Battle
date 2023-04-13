using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public class PiercingAmmo : Ammo // бронебойный 
    {
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override RectangleF GetCollider()
        {
            throw new NotImplementedException();
        }

        public override int GetDistance()
        {
            return 120;
        }

        public override int GetRadius()
        {
            return 45;
        }

        public override Vector2 GetSpeed()
        {
            return new Vector2(0.003f, 0.0f);
        }

        public override void Update()
        {
            if (isLeft) // для проверки, в какую сторону летит снаряд
                PositionCenter -= GetSpeed();
            else
                PositionCenter += GetSpeed();
        }
    }
}
