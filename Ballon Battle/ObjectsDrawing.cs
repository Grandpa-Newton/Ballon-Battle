using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Ballon_Battle
{
    public class ObjectsDrawing
    {
        public static void Draw(Texture texture, Vector2[] position)
        {
            Vector2[] vertices = new Vector2[4] // вершины спрайта
            {
                new Vector2(0.0f,1.0f),
                new Vector2(1.0f,1.0f),
                new Vector2(1.0f,0.0f),
                new Vector2(0.0f,0.0f),
            };



            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, texture.Id);

            GL.Begin(PrimitiveType.Quads);

        //    GL.Color3(color);

            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);

                GL.Vertex2(position[i]);
            }

            GL.End();
        }

        public static void Start()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //   GL.Ortho(-width/2f, width/2f, height/2f, -height/2f, 0f, 1f);
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
    }
}
