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
                /*TextureDrawer.LoadTexure("Animation/animation_1.png"),
                TextureDrawer.LoadTexure("Animation/animation_2.png"),
                TextureDrawer.LoadTexure("Animation/animation_3.png"),
                TextureDrawer.LoadTexure("Animation/animation_4.png"),
                TextureDrawer.LoadTexure("Animation/animation_5.png"),
                TextureDrawer.LoadTexure("Animation/animation_6.png"),
                TextureDrawer.LoadTexure("Animation/animation_7.png"),*/
                TextureDrawer.LoadTexure("Animation/1.png"),
                TextureDrawer.LoadTexure("Animation/2.png"),
                TextureDrawer.LoadTexure("Animation/3.png"),
                TextureDrawer.LoadTexure("Animation/4.png"),
                TextureDrawer.LoadTexure("Animation/5.png"),
            };
        }

        public void Draw()
        {
            try
            {
                ObjectsDrawing.Draw(animation[Count], position);
            }
            catch
            {
                throw new Exception("EndOfAnimationException");
            }
            
            
            Count++;

        }

        
    }
}
