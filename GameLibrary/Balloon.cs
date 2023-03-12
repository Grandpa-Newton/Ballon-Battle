using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Balloon
    {
        public int Armour { get; set; } = 0;
        public int Health { get; set; } = 100;
        public bool CheckAlive(int health, int armour)
        {
            if (health <= 0)
                return false;
            else
                return true;
             
        }
    }
}
