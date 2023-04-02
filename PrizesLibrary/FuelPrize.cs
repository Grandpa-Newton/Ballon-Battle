using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    internal class FuelPrize : Prize
    {
        public FuelPrize(double speed) : base(speed)
        {
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
