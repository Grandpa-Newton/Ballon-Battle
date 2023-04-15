using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsOpenGL;
using OpenTK;


namespace AmmoLibrary
{
    public class SupersonicAmmo : Ammo // сверхзвуковой
    {
        public SupersonicAmmo(Vector2 position, bool isLeft, Texture sprite)
        {
            this.sprite = sprite;
            this.PositionCenter = position;
            this.isLeft = isLeft;
            //this.Distance = 80;
            //this.Radius = 30;
            //this.Speed = new Vector2(0.025f, 0.0f);
        }

        public SupersonicAmmo(Texture sprite)
        {
            this.sprite = sprite;
            this.Distance = 80;
            this.Radius = 30;
            this.Speed = new Vector2(0.025f, 0.0f);
        }
        public override void Draw()
        {
            //ObjectsDrawing.Draw(sprite, GetPosition());
            GetPosition();
            if(isLeft)
            {
                ObjectsDrawing.Draw(sprite, Position, false);
            }
            else
                ObjectsDrawing.Draw(sprite, Position, true);

        }

        public override void GetPosition()
        {
            Position = new Vector2[4]
            {
                PositionCenter + new Vector2(-0.03f, -0.0125f),
                PositionCenter + new Vector2(0.03f, -0.0125f),
                PositionCenter + new Vector2(0.03f, 0.0125f),
                PositionCenter + new Vector2(-0.03f, 0.0125f),
            };
        }

        /*public override Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-0.03f, -0.0125f),
                PositionCenter + new Vector2(0.03f, -0.0125f),
                PositionCenter + new Vector2(0.03f, 0.0125f),
                PositionCenter + new Vector2(-0.03f, 0.0125f),
            };
        }*/

        public override int GetDistance()
        {
            return 80;
        }

        public override int GetRadius()
        {
            return 30;
        }

        public override Vector2 GetSpeed()
        {
            return new Vector2(0.025f, 0.0f);
        }

        public override void Update()
        {
            /*if (isLeft) // для проверки, в какую сторону летит снаряд
                PositionCenter -= GetSpeed();
            else
                PositionCenter += GetSpeed();*/
            if (isLeft)
                PositionCenter -= Speed;
            else
                PositionCenter += Speed;
        }

        public override RectangleF GetCollider()
        {
            //  Vector2[] colliderPosition = GetPosition(); // добавить более точную коллизию!
            GetPosition();

            Vector2[] colliderPosition = Position;

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        public override void Spawn(Vector2 position, bool isLeft)
        {
            this.PositionCenter = position;
            this.isLeft = isLeft;
        }
    }
}
