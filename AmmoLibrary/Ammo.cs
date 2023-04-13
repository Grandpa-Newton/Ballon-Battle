using Ballon_Battle;
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
        protected Texture sprite;
        public Vector2 PositionCenter;
        protected bool isLeft;
        public abstract void Update();
        public abstract Vector2 GetSpeed();
        public abstract int GetDistance();
        public abstract int GetRadius();
        public abstract void Draw();
        public abstract RectangleF GetCollider();
    }
}
