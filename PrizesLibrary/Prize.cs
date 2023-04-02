﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
    public abstract class Prize
    {
        public double speed;
        public Prize(double speed)
        {
            this.speed = speed;
        }

        public abstract void Draw();
    }
}
