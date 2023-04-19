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
    public class ExplosiveAmmo : Ammo // фугасный
    {
        public ExplosiveAmmo(Texture sprite) : base(sprite)
        {
            this.Sprite = sprite;
            this.Distance = 1.2f;
            this.Radius = 0.2f;
            this.Speed = new Vector2(0.009f, 0.0f);
        }
        public ExplosiveAmmo(Ammo clone) : base(clone) // для копирования объектов, а не ссылок
        {
        }
        
    }
}
