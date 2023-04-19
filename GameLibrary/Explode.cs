using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsOpenGL;

namespace GameLibrary
{
    public class Explode
    {
        List<Texture> animation;
        Vector2[] position;
        public int Count; // отвечает за номер анимации
        public Explode(Vector2[] position)
        {
            this.position = position;
            this.Count = 0;

            this.animation = new List<Texture>()
            {
                TextureLoader.LoadTexure("Animation/1.png"),
                TextureLoader.LoadTexure("Animation/2.png"),
                TextureLoader.LoadTexure("Animation/3.png"),
                TextureLoader.LoadTexure("Animation/4.png"),
                TextureLoader.LoadTexure("Animation/5.png"),
            };
        }

        public bool Draw(bool isFlipped)
        {
            if(Count >= animation.Count)
            {
                return false;
            }

            ObjectDrawer.Draw(animation[Count], position, isFlipped);

            Count++;

            return true;
        }
    }
}
