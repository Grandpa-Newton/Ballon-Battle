using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ballon_Battle;
using GameLibrary;
using OpenTK;


namespace AmmoLibrary
{
    public class SupersonicAmmo : Ammo // сверхзвуковой
    {
        public SupersonicAmmo(Vector2 position, bool isLeft)
        {
            this.sprite = TextureDrawer.LoadTexure("supersonicAmmo.png");
            this.PositionCenter = position;
            this.isLeft = isLeft;
        }
        public override void Draw()
        {
            ObjectsDrawing.Draw(sprite, getPosition());
        }

        private Vector2[] getPosition()
        {
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-0.05f, -0.1f),
                PositionCenter + new Vector2(0.05f, -0.1f),
                PositionCenter + new Vector2(0.05f, 0.1f),
                PositionCenter + new Vector2(-0.05f, 0.1f),
            };
        }

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
            return new Vector2(0.01f, 0.0f);
        }

        public override void Update()
        {
            if (isLeft) // для проверки, в какую сторону летит снаряд
                PositionCenter -= GetSpeed();
            else
                PositionCenter += GetSpeed();
        }
    }
}
