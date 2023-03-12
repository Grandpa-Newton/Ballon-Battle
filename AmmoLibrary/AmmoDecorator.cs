using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    internal class AmmoDecorator : Ammo
    {
        private Ammo ammo;

        public AmmoDecorator (Ammo ammo)
        {
            this.ammo = ammo;
        }
        public override double Speed { get => ammo.Speed; set => ammo.Speed = value; } // добавить множитель, который буду получать
                                                                                       // из призов, и в set домножать значения на этот множитель
                                                                                       // (?) 

        public override int Distancde { get => ammo.Distancde; set => ammo.Distancde = value; }
        public override int Radius { get => ammo.Radius; set => ammo.Radius = value; }
    }
}
