using Ballon_Battle;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary.Generators
{
    public class AmmoGenerator : PrizeGenerator
    {
        public AmmoGenerator()
        {
            sprite = TextureDrawer.LoadTexure("ammoPrize.png");
        }
        public override Prize Create(Vector2 centerPosition, bool isLeft)
        {
            return new AmmoPrize(centerPosition, isLeft, sprite);
        }
    }
}
