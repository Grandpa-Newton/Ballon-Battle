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
            this.sprite = ammo.sprite;
            /* public Texture sprite;
         public Vector2 PositionCenter;
         protected bool isLeft;

         public Vector2 Speed { get; set; }

         public int Distance { get; set; }

         public int Radius { get; set; }

         public Vector2[] Position { get; set; }*/

            this.Distance = (int)(ammo.Distance * 1.5);
            this.Radius = ammo.Radius;
            // ammo.Speed *= 2.5f;
            this.Speed = ammo.Speed;
            // ammo.GetPosition();
            // this.Position = ammo.Position;
        }

        public override void Draw()
        {
            GetPosition();
            if (isLeft)
            {
                ObjectsDrawing.Draw(sprite, Position, false);
            }
            else
                ObjectsDrawing.Draw(sprite, Position, true);
        }

        public override RectangleF GetCollider()
        {
            GetPosition();

            Vector2[] colliderPosition = Position;

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        public override int GetDistance()
        {
            return ammo.Distance;
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

        public override int GetRadius()
        {
            return ammo.Radius;
        }

        public override Vector2 GetSpeed()
        {
            ammo.Speed *= 3.5f;

            return ammo.Speed;
        }

        public override void Spawn(Vector2 position, bool isLeft)
        {
            this.PositionCenter = position;
            this.isLeft = isLeft;
        }

        public override void Update()
        {
            if (isLeft)
                PositionCenter -= Speed;
            else
                PositionCenter += Speed;
            //     ammo.Update();
        }
    }
}
