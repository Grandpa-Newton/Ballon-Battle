using GraphicsOpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public class PiercingAmmo : Ammo // бронебойный 
    {
        public PiercingAmmo(Texture sprite)
        {
            this.sprite = sprite;
            this.Distance = 1.5f;
            this.Radius = 0.2f;
            this.Speed = new Vector2(0.01f, 0.0f);
        }
        public PiercingAmmo(Ammo clone) // для копирования объектов, а не ссылок
        {
            this.sprite = clone.sprite;
            PositionCenter = clone.PositionCenter;
            this.isLeft = clone.isLeft;
            Speed = clone.Speed;
            Distance = clone.Distance;
            Radius = clone.Radius;
            Position = clone.Position;
        }
        public override void Draw()
        {
            GetPosition(false);
            if (isLeft)
            {
                ObjectsDrawing.Draw(sprite, Position, false);
            }
            else
                ObjectsDrawing.Draw(sprite, Position, true);
        }

        public override RectangleF GetCollider(bool isExploding)
        {
            GetPosition(isExploding);

            Vector2[] colliderPosition = Position;

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

       /* public override RectangleF GetExplodeCollider()
        {
            GetPosition(true);

            return GetCollider();
        }*/

        public override float GetDistance()
        {
            return 1.5f;
        }

        public override void GetPosition(bool isExploding)
        {
            if (isExploding)
            {
                Position = new Vector2[4]
                {
                    PositionCenter + new Vector2(-0.03f, -0.0125f) + new Vector2(-Radius, -Radius),
                    PositionCenter + new Vector2(0.03f, -0.0125f) + new Vector2(Radius, -Radius),
                    PositionCenter + new Vector2(0.03f, 0.0125f) + new Vector2(Radius, Radius),
                    PositionCenter + new Vector2(-0.03f, 0.0125f) + new Vector2(-Radius, Radius),
                };
            }
            else
            {
                Position = new Vector2[4]
                {
                    PositionCenter + new Vector2(-0.03f, -0.0125f),
                    PositionCenter + new Vector2(0.03f, -0.0125f),
                    PositionCenter + new Vector2(0.03f, 0.0125f),
                    PositionCenter + new Vector2(-0.03f, 0.0125f),
                };
            }

        }

        public override float GetRadius()
        {
            return 0.2f;
        }

        public override Vector2 GetSpeed()
        {
            return new Vector2(0.003f, 0.0f);
        }

        public override void Spawn(Vector2 position, bool isLeft)
        {
            this.PositionCenter = position;
            this.isLeft = isLeft;
        }

        public override void Update()
        {
            if (isLeft) // для проверки, в какую сторону летит снаряд
                PositionCenter -= Speed;
            else
                PositionCenter += Speed;

            Distance -= Speed.X;
        }
    }
}
