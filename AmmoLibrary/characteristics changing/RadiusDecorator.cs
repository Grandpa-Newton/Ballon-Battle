using GraphicsOpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary.characteristics_changing
{
    public class RadiusDecorator : AmmoDecorator
    {
        public RadiusDecorator(Ammo ammo) : base(ammo)
        {
            this.Sprite = ammo.Sprite;

            this.Distance = ammo.Distance;
            this.Radius = ammo.Radius * 1.2f;
            this.Speed = ammo.Speed;
        }
    }
}
