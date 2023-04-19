using GraphicsOpenGL;
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
            this.Sprite = ammo.Sprite;
            this.Distance = ammo.Distance;
            this.Radius = ammo.Radius;
            this.Speed = ammo.Speed * 1.3f;
        }
    }
}
