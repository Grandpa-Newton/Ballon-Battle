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
    public class HealthPrize : Prize
    {
        public HealthPrize(Vector2 centerPosition, bool isLeft) : base(centerPosition, isLeft)
        {
            this.centerPosition = centerPosition;
            this.isLeft = isLeft;
            this.sprite = TextureLoader.LoadTexure("healthPrize.png");
        }

        protected override Vector2 GetSpeed()
        {
            return new Vector2(0.005f, 0.0f);
        }
    }
}
