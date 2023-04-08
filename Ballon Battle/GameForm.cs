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
using GameLibrary;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        Texture backgroundTexture;
        Balloon firstPlayer;
        
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
            glTimer.Start();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
           
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

        //    GL.ClearColor(new Color4(0.631f, 0.6f, 0.227f, 1f));

            backgroundTexture = TextureDrawer.LoadTexure("clouds.jpg");

            Texture firstPlayerTexture = TextureDrawer.LoadTexure("testBalloon.png");

            firstPlayer = new Balloon(new Vector2(-0.5f, 0.0f), firstPlayerTexture);
        }
        
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            // Добавление фона на картинку

            

            //GL.Enable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, backgroundTexture.Id);

            //GL.Begin(PrimitiveType.Quads);

            //GL.TexCoord2(0.0f, 1.0f);
            //GL.Vertex2(-1.0f, -1.0f);
            //GL.TexCoord2(1.0f, 1.0f);
            //GL.Vertex2(1.0f, -1.0f);
            //GL.TexCoord2(1.0f, 0.0f);
            //GL.Vertex2(1.0f, 1.0f);
            //GL.TexCoord2(0.0f, 0.0f);
            //GL.Vertex2(-1.0f, 1.0f);

            // -1 -1
            // 1 -1
            // 1 1
            // -1 1

            //   GL.ClearTexImage(backgroundTexture, 1, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.);

            //GL.End();

            Draw();

            glControl.SwapBuffers();
        }

        private void Draw()
        {
            ObjectsDrawing.Start();

            //    ObjectsDrawing.Start(this.Width, this.Height);

            //   ObjectsDrawing.Draw(backgroundTexture, new Vector2(-1.0f, 1.0f), new Vector2(0.1f, 0.1f), Vector2.Zero);

            ObjectsDrawing.Draw(backgroundTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            });

            firstPlayer.Draw();
        }

        private void UpdateGame()
        {
            //firstPlayer.Update();
        }

        private void glTimer_Tick(object sender, EventArgs e) // для обновления картинки каждые 16 миллисекунд (чуть больше 60 фреймов в секунде)
        {
       //     firstPlayer.PositionCenter += new Vector2(0.5f, 0.5f);
            
            UpdateGame();

            glControl.Refresh();
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            glControl.Size = this.Size;
            GL.Viewport(0, 0, Width, Height);
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                firstPlayer.PositionCenter += new Vector2(0.01f, 0.01f);
        }
    }

        
}
