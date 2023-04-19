using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace GraphicsOpenGL
{
    public class ObjectDrawer
    {
        public static void Draw(Texture texture, Vector2[] position, bool isFlipped)
        {
            Start();

            Vector2[] vertices;

            if (isFlipped) // отзеркалить ли текстуру
            {
                vertices = new Vector2[4] // вершины спрайта
                {
                    new Vector2(1.0f,1.0f), // правый низ
                    new Vector2(0.0f,1.0f), // правый верх
                    new Vector2(0.0f,0.0f), // левый верх
                    new Vector2(1.0f,0.0f), // левый низ
                };
            }
            else
            {
                vertices = new Vector2[4] // вершины спрайта
                {
                    new Vector2(0.0f,1.0f), // левый низ
                    new Vector2(1.0f,1.0f), // правый низ
                    new Vector2(1.0f,0.0f), // правый верх
                    new Vector2(0.0f,0.0f), // левый верх
                };
            }

            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, texture.Id);

            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);

                GL.Vertex2(position[i]);
            }

            GL.End();
        }

        private static void Start()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
    }
}
