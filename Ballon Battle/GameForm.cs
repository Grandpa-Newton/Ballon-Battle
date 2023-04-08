using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        int backgroundTexture;
        public GameForm()
        {
            InitializeComponent();
            CenterToScreen();
            glControl.Visible = false;
            
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Visible = false;
            glControl.Visible = true;
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

            GL.Enable(EnableCap.Texture2D);
           
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

        //    GL.ClearColor(new Color4(0.631f, 0.6f, 0.227f, 1f));

            backgroundTexture = TextureDrawer.LoadTexure("clouds.jpg");
        }
        
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            // Добавление фона на картинку

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, backgroundTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, 1.0f);

            GL.End();

            //   GL.ClearTexImage(backgroundTexture, 1, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.);

            Draw();

            glControl.SwapBuffers();
        }

        private void Draw()
        {

        }

        private void UpdateGame()
        {

        }

        private void glTimer_Tick(object sender, EventArgs e) // для обновления картинки каждые 16 миллисекунд (чуть больше 60 фреймов в секунде)
        {

            Update();

            glControl.Invalidate();
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            glControl.Size = this.Size;


        }
    }

        
}
