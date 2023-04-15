using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsOpenGL
{
    public class Texture
    {
        public int Id;

        public int Width;

        public int Height;

        public Texture(int id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }
    }
}
