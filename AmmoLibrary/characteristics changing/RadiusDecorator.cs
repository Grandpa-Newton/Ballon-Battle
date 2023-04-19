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
    public class RadiusDecorator : AmmoDecorator
    {
        public RadiusDecorator(Ammo ammo) : base(ammo)
        {
            this.sprite = ammo.sprite;
            /* public Texture sprite;
         public Vector2 PositionCenter;
         protected bool isLeft;

         public Vector2 Speed { get; set; }

         public int Distance { get; set; }

         public int Radius { get; set; }

         public Vector2[] Position { get; set; }*/

            this.Distance = ammo.Distance;
            this.Radius = ammo.Radius * 1.2f;
            // ammo.Speed *= 2.5f;
            this.Speed = ammo.Speed;
            // ammo.GetPosition();
            // this.Position = ammo.Position;
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
        public override void GetPosition(bool isExploding)
        {
            if (isExploding)
            {
                Position = new Vector2[4]
                {
                    PositionCenter + new Vector2(-0.03f, -0.0125f) + new Vector2(Radius, Radius),
                    PositionCenter + new Vector2(0.03f, -0.0125f) + new Vector2(Radius, Radius),
                    PositionCenter + new Vector2(0.03f, 0.0125f) + new Vector2(Radius, Radius),
                    PositionCenter + new Vector2(-0.03f, 0.0125f) + new Vector2(Radius, Radius),
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
     /*   public override RectangleF GetExplodeCollider()
        {
            GetPosition(true);

            return GetCollider();
        }*/


    }
}
