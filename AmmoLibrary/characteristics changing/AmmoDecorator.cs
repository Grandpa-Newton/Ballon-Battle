using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public abstract class AmmoDecorator : Ammo
    {
        protected Ammo ammo;

        public AmmoDecorator(Ammo ammo) : base(ammo)
        {
            this.ammo = ammo;
        }
    }
}
