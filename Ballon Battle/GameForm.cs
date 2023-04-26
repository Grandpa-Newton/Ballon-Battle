using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using GameLibrary;
using System.Diagnostics;
using AmmoLibrary;
using PrizesLibrary;
using GraphicsOpenGL;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        BattleGame gameEngine; // объект игрового движка
        Label firstPlayerInfo; // label для отображения текущего состояния первого игрока
        Label secondPlayerInfo; // label для отображения текущего состояния второго игрока

        public GameForm()
        {

            InitializeComponent();
            CenterToScreen();
            glControl.Size = this.Size;
            glControl.Visible = true;
            glTimer.Start();
            prizeTimer.Start();
            windTimer.Start();
            gameEngine = new BattleGame();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

            gameEngine.LoadGLControl();

            gameEngine.LoadObjects();

            glControl.SendToBack();

            this.WindowState = FormWindowState.Maximized; // для открытия окна в полном экране
        }
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit); // ?

            gameEngine.Draw();

            glControl.SwapBuffers();
        }

        private void UpdateInfo()
        {
            firstPlayerInfo.SetBounds((int)(0.05 * Width), (int)(0.01 * Height), (int)(0.45 * Width), (int)(0.03 * Height)); // информация первого игрока (здоровье, топливо, броня)
            firstPlayerInfo.Font = new Font("Arial", 0.008f * Width);
            firstPlayerInfo.Text = gameEngine.GetFirstPlayerInfo();

            secondPlayerInfo.SetBounds((int)(0.55 * Width), (int)(0.01 * Height), (int)(0.45 * Width), (int)(0.03 * Height)); // информация первого игрока (здоровье, топливо, броня)
            secondPlayerInfo.Font = new Font("Arial", 0.008f * Width);
            secondPlayerInfo.Text = gameEngine.GetSecondPlayerInfo();
        }

        private void EndGame(string message)
        {
            glTimer.Stop();
            DialogResult result = MessageBox.Show(message, "Конец игры", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            else
                this.Close();
        }

        private void glTimer_DrawTick(object sender, EventArgs e)
        {
            glControl.Refresh();

            if (gameEngine.GetExplodesCount() <= 0)
            {
                EndGame("ИГРА ОКОНЧЕНА! НИЧЬЯ! Хотите начать заново?");
            }
        }
        private void glTimer_FirstPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            if (gameEngine.GetExplodesCount() <= 0)
            {
                EndGame("ИГРА ОКОНЧЕНА! ПЕРВОЙ ИГРОК ПРОИГРАЛ. Хотите начать заново?");
            }
        }
        private void glTimer_SecondPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            if (gameEngine.GetExplodesCount() <= 0)
            {
                EndGame("ИГРА ОКОНЧЕНА! ВТОРОЙ ИГРОК ПРОИГРАЛ. Хотите начать заново?");
            }
        }

        private void windTimer_Tick(object sender, EventArgs e)
        {
            gameEngine.UpdateWind();
        }

        private void glTimer_Tick(object sender, EventArgs e) // для обновления картинки каждые 10 миллисекунд (100 фреймов в секунду)
        {
            int resultCode = gameEngine.Update();

            switch(resultCode)
            {
                case 0:
                    break;
                case 1:
                    glTimer.Stop();
                    prizeTimer.Stop();
                    windTimer.Stop();
                    glTimer.Tick -= glTimer_Tick;
                    
                    glTimer.Tick += glTimer_FirstPlayerLooseTick;
                    glTimer.Start();
                    break;
                case 2:
                    glTimer.Stop();
                    prizeTimer.Stop();
                    windTimer.Stop();
                    glTimer.Tick -= glTimer_Tick;
                    glTimer.Tick += glTimer_SecondPlayerLooseTick;
                    glTimer.Start();
                    break;
                case 3:
                    glTimer.Stop();
                    prizeTimer.Stop();
                    windTimer.Stop();
                    glTimer.Tick -= glTimer_Tick;
                    glTimer.Tick += glTimer_DrawTick;
                    glTimer.Start();
                    break;

            }

            UpdateInfo();

            glControl.Refresh();
        }

        private void prizeTimer_Tick(object sender, EventArgs e)
        {
            gameEngine.SpawnPrize(this.Width, this.Height);

        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            glControl.Size = this.Size;
            GL.Viewport(0, 0, Width, Height);
        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            gameEngine.UpdateKeyUp(e);
        }
        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            gameEngine.UpdateKeyDown(e);
        }
    }
}
