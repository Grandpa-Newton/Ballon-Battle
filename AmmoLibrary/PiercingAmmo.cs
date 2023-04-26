using GraphicsOpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmoLibrary
{
    public class PiercingAmmo : Ammo // бронебойный 
    {
        
        public PiercingAmmo() : base()
        {
            this.Sprite = TextureLoader.LoadTexure("piercingAmmo.png");
            this.Distance = 1.8f;
            this.Radius = 0.1f;
            this.Speed = new Vector2(0.012f, 0.0f);
        }
        public PiercingAmmo(Ammo clone) : base(clone) // для копирования объектов, а не ссылок
        {
        }
    }
}
