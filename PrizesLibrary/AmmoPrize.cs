using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    internal class AmmoPrize : Prize
    {
        public AmmoPrize(double speed) : base(speed)
        {
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
