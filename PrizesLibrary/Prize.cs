using GraphicsOpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    public abstract class Prize
    {
        protected Vector2 centerPosition;
        protected bool isLeft;
        protected Texture sprite;
        protected abstract Vector2 getSpeed();
        public abstract void Draw();
        public abstract RectangleF GetCollider();

        protected abstract Vector2[] getPosition();

        public abstract void Update();

    }
}
