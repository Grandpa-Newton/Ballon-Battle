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
                TextureDrawer.LoadTexure("Animation/1.png"),
                TextureDrawer.LoadTexure("Animation/2.png"),
                TextureDrawer.LoadTexure("Animation/3.png"),
                TextureDrawer.LoadTexure("Animation/4.png"),
                TextureDrawer.LoadTexure("Animation/5.png"),
            };
        }

        public bool Draw(bool isFlipped)
        {
            if(Count >= animation.Count)
            {
                return false;
            }

            ObjectsDrawing.Draw(animation[Count], position, isFlipped);

            Count++;

            return true;
        }
    }
}
