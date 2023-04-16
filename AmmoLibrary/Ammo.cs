﻿using GraphicsOpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public abstract class Ammo
    {
        public Texture sprite;
        public Vector2 PositionCenter;
        public bool isLeft;

        public Vector2 Speed { get; set; }

        public float Distance { get; set; }

        public float Radius { get; set; }

        public Vector2[] Position { get; set; }
        public abstract void Update();
        public abstract Vector2 GetSpeed();
        public abstract float GetDistance();
        public abstract float GetRadius();
        public abstract void Draw();
        public abstract RectangleF GetCollider(bool isExploding);

    //    public abstract RectangleF GetExplodeCollider();

    //    public abstract Vector2[] GetPosition();

        public abstract void GetPosition(bool isExploding);
        public abstract void Spawn(Vector2 position, bool isLeft);
    }
}
