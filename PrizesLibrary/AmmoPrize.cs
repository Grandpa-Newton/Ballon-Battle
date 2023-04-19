using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsOpenGL;

namespace PrizesLibrary
{
    public class AmmoPrize : Prize
    {
        public AmmoPrize(Vector2 centerPosition, bool isLeft, Texture sprite) : base(centerPosition, isLeft, sprite)
        {
        }
        protected override Vector2 getSpeed()
        {
            return new Vector2(0.005f, 0.0f);
        }
    }
}
