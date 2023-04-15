﻿using Ballon_Battle;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{

    public class HealthGenerator : PrizeGenerator
    {
        public HealthGenerator()
        {
            sprite = TextureDrawer.LoadTexure("healthPrize.png");
        }

        public override Prize Create(Vector2 centerPosition, bool isLeft)
        {
            return new HealthPrize(centerPosition, isLeft, sprite);
        }
    }
}
