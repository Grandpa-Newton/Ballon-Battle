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
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            glControl.Visible = false;
            CenterToScreen();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Visible = false;
            glControl.Visible = true;
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.ClearColor(new Color4(0.631f, 0.6f, 0.227f, 1f));
        }
        
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Draw();

            glControl.SwapBuffers();
        }

        private void Draw()
        {

        }

        private void Update()
        {

        }

        private void glTimer_Tick(object sender, EventArgs e) // для обновления картинки каждые 16 миллисекунд (чуть больше 60 фреймов в секунде)
        {

            Update();

            glControl.Invalidate();
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            glControl.Size = this.Size;

        }
    }

        
}
