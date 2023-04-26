using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsOpenGL;
using OpenTK;


namespace AmmoLibrary
{
    public class SupersonicAmmo : Ammo // сверхзвуковой
    {
        public SupersonicAmmo() : base()
        {
            this.Sprite = TextureLoader.LoadTexure("supersonicAmmo.png");
            this.Distance = 1.0f;
            this.Radius = 0.05f;
            this.Speed = new Vector2(0.0145f, 0.0f);
        }

        public SupersonicAmmo(Ammo clone) : base(clone) // для копирования объектов, а не ссылок
        {
        }
    }
}
