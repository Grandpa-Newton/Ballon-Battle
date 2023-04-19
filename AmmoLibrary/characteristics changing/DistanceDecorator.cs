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
    public class DistanceDecorator : AmmoDecorator
    {
        public DistanceDecorator(Ammo ammo) : base(ammo)
        {
            this.Sprite = ammo.Sprite;
            this.Distance = ammo.Distance * 1.25f;
            this.Radius = ammo.Radius;
            this.Speed = ammo.Speed;
        }
    }
}
