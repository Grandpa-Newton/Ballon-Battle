using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    internal class SupersonicAmmo : Ammo // сверхзвуковой
    {
        public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Distancde { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Radius { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
