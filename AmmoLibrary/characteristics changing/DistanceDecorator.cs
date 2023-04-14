﻿using OpenTK;
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
        }

        public override void Draw()
        {
            ammo.Draw();
        }

        public override int GetDistance() // как в speed
        {
            return (int)(ammo.GetDistance()*1.2);
        }

        public override int GetRadius()
        {
            return ammo.GetRadius();
        }

        public override Vector2 GetSpeed()
        {
            return ammo.GetSpeed();
        }

        public override void Update()
        {
            ammo.Update();
        }
        public override RectangleF GetCollider()
        {
            return ammo.GetCollider();
        }

        public override Vector2[] GetPosition()
        {
            return ammo.GetPosition();
        }
    }
}
