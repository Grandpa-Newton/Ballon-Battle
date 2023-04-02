﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    internal class SupersonicAmmo : Ammo // сверхзвуковой
    {
        public override int GetDistance()
        {
            return 80;
        }

        public override int GetRadius()
        {
            return 30;
        }

        public override double GetSpeed()
        {
            return 10.0;
        }
    }
}
