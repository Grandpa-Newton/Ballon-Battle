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
        protected abstract Vector2 GetSpeed();
        public void Draw(bool isFlipped)
        {
            ObjectDrawer.Draw(sprite, GetPosition(), isFlipped);
        }
        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition(); // добавить более точную коллизию!


            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }
        protected Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                centerPosition + new Vector2(-0.06f, -0.06f),
                centerPosition + new Vector2(0.06f, -0.06f),
                centerPosition + new Vector2(0.06f, 0.06f),
                centerPosition + new Vector2(-0.06f, 0.06f),
            };
        }

        public void Update()
        {
            if (isLeft) // для проверки, в какую сторону летит приз
                centerPosition -= GetSpeed();
            else
                centerPosition += GetSpeed();
        }
        public Prize(Vector2 centerPosition, bool isLeft, Texture sprite)
        {
            this.centerPosition = centerPosition;
            this.isLeft = isLeft;
            this.sprite = sprite;
        }

    }
}
