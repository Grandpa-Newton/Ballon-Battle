using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    public abstract class PrizeGenerator
    {

        public abstract Prize Create(double speed);


    }
}
