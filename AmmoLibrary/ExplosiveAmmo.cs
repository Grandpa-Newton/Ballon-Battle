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
    public class ExplosiveAmmo : Ammo // фугасный
    {
        public ExplosiveAmmo(Texture sprite)
        {
            this.sprite = sprite;
        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override RectangleF GetCollider()
        {
            throw new NotImplementedException();
        }

        public override int GetDistance()
        {
            return 100;
        }

        public override Vector2[] GetPosition()
        {
            throw new NotImplementedException();
        }

        public override int GetRadius()
        {
            return 50;
        }

        public override Vector2 GetSpeed()
        {
            return new Vector2(0.005f, 0.0f);
        }

        public override void Spawn(Vector2 position, bool isLeft)
        {
            this.PositionCenter = position;
            this.isLeft = isLeft;
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
