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
    public class ArmourPrize : Prize
    {
        public ArmourPrize(Vector2 centerPosition, bool isLeft, Texture sprite)
        {
            this.centerPosition = centerPosition;
            this.isLeft = isLeft;
            this.sprite = sprite;
        }

        public override void Draw()
        {
            ObjectsDrawing.Draw(sprite, getPosition());
        }

        public override RectangleF GetCollider()
        {
            Vector2[] colliderPosition = getPosition(); // добавить более точную коллизию!


            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        public override void Update()
        {
            if (isLeft) // для проверки, в какую сторону летит приз
                centerPosition -= getSpeed();
            else
                centerPosition += getSpeed();
        }

        protected override Vector2[] getPosition()
        {
            return new Vector2[4]
            {
                centerPosition + new Vector2(-0.06f, -0.06f),
                centerPosition + new Vector2(0.06f, -0.06f),
                centerPosition + new Vector2(0.06f, 0.06f),
                centerPosition + new Vector2(-0.06f, 0.06f),
            };
        }

        protected override Vector2 getSpeed()
        {
            return new Vector2(0.005f, 0.0f);
        }
    }
}
