﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    internal class ArmourGenerator : PrizeGenerator
    {
        public override Prize Create()
        {
            return new ArmourPrize();
        }
    }
}
