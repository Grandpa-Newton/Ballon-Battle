using GraphicsOpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public abstract class Ammo
    {
        public Texture Sprite;
        public Vector2 PositionCenter;
        public bool isLeft;

        public Vector2 Speed { get; set; }

        public float Distance { get; set; }

        public float Radius { get; set; }

        public Vector2[] Position { get; set; }

        public Ammo(Ammo clone)
        {
            this.Sprite = clone.Sprite;
            PositionCenter = clone.PositionCenter;
            this.isLeft = clone.isLeft;
            Speed = clone.Speed;
            Distance = clone.Distance;
            Radius = clone.Radius;
            Position = clone.Position;
        }

        public Ammo()
        {

        }

     /*   public Ammo(Texture sprite)
        {
            this.Sprite = sprite;
        }
           public abstract Vector2 GetSpeed();
           public abstract float GetDistance();
           public abstract float GetRadius();*/
        public void Draw()
        {
            UpdatePosition(false);
            if (isLeft)
            {
                ObjectDrawer.Draw(Sprite, Position, false);
            }
            else
                ObjectDrawer.Draw(Sprite, Position, true);
        }

        public RectangleF GetCollider(bool isExploding = false)
        {
            UpdatePosition(isExploding);

            Vector2[] colliderPosition = Position;

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        public void Spawn(Vector2 position, bool isLeft)
        {
            this.PositionCenter = position;
            this.isLeft = isLeft;
        }

        public void Update()
        {
            if (isLeft) // для проверки, в какую сторону летит снаряд
                PositionCenter -= Speed;
            else
                PositionCenter += Speed;

            Distance -= Speed.X;
        }

        public virtual void UpdatePosition(bool isExploding)
        {
            float spriteWidth = 0.03f;
            float spriteHeight = 0.0125f;
            Position = new Vector2[4]
            {
                PositionCenter + new Vector2(-spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, spriteHeight),
                PositionCenter + new Vector2(-spriteWidth, spriteHeight),
            };

            if (isExploding)
            {
                Position[0] += new Vector2(-Radius, -Radius);
                Position[1] += new Vector2(Radius, -Radius);
                Position[2] += new Vector2(Radius, Radius);
                Position[3] += new Vector2(-Radius, Radius);
            }
        }
    }
}
