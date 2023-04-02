using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    internal class HealthPrize : Prize
    {
        public HealthPrize(double speed) : base(speed)
        {
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
