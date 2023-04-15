using GraphicsOpenGL;
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
        protected bool isLeft;

        public Vector2 Speed { get; set; }

        public int Distance { get; set; }

        public int Radius { get; set; }

        public Vector2[] Position { get; set; }
        public abstract void Update();
        public abstract Vector2 GetSpeed();
        public abstract int GetDistance();
        public abstract int GetRadius();
        public abstract void Draw();
        public abstract RectangleF GetCollider();

    //    public abstract Vector2[] GetPosition();

        public abstract void GetPosition();
        public abstract void Spawn(Vector2 position, bool isLeft);
    }
}
