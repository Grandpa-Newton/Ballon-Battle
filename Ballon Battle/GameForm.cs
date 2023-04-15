﻿using System;
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
//using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using GameLibrary;
using System.Diagnostics;
using AmmoLibrary;
using PrizesLibrary;
using PrizesLibrary.Generators;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        Texture backgroundTexture;
        Texture landTexture;
        Texture bulletTexture;
        Balloon firstPlayer;
        Balloon secondPlayer;
        RectangleF landCollider;
        RectangleF screenCollider; // границы экрана
        List<Ammo> firstAmmos; // текущий снаряд первого игрока
        List<Ammo> secondAmmos; // текущий снаряд второго игрока4
        List<Explode> explodes; // анимации взрывов
        Random random = new Random();
        Prize currentPrize = null;

        bool isWdown, isSdown, isIdown, isKdown, isJdown, isDdown;
        
        public GameForm()
        {
            
            InitializeComponent();
            CenterToScreen();
            glControl.Size = this.Size;
            glControl.Visible = false;
            isWdown = false;
            isSdown = false;
            isKdown = false;
            isKdown = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Visible = false;
            glControl.Visible = true;
            glTimer.Start();
            prizeTimer.Start();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend); // для отключения фона у ассетов
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
           
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

        //    GL.ClearColor(new Color4(0.631f, 0.6f, 0.227f, 1f));

            backgroundTexture = TextureDrawer.LoadTexure("clouds.jpg");

            Texture firstPlayerTexture = TextureDrawer.LoadTexure("testBalloon.png");

            firstPlayer = new Balloon(new Vector2(-0.7f, 0.0f), firstPlayerTexture);

            Texture secondPlayerTexture = TextureDrawer.LoadTexure("testBalloon_2.png");

            secondPlayer = new Balloon(new Vector2(0.7f, 0.0f), secondPlayerTexture);

            landTexture = TextureDrawer.LoadTexure("grasstexture_new.png");

            firstAmmos = new List<Ammo>();

            secondAmmos = new List<Ammo>();

            screenCollider = new RectangleF(0.0f, 0.125f, 1.0f, 0.875f);

            explodes = new List<Explode>();
        }
        
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            Draw();

            glControl.SwapBuffers();
        }

        private void Draw()
        {

            //    ObjectsDrawing.Start(this.Width, this.Height);

            //   ObjectsDrawing.Draw(backgroundTexture, new Vector2(-1.0f, 1.0f), new Vector2(0.1f, 0.1f), Vector2.Zero);

            ObjectsDrawing.Draw(backgroundTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            });

            ObjectsDrawing.Draw(landTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, -0.75f),
                new Vector2(-1.0f, -0.75f),
            });

            landCollider = new RectangleF(0.0f, 0.875f, 1.0f, 0.125f);

            firstPlayer.Draw();

            secondPlayer.Draw();

            foreach (var item in firstAmmos)
            {
                item.Draw();
            }
            foreach (var item in secondAmmos)
            {
                item.Draw();
            }
            for (int i = 0; i < explodes.Count; i++)
            {
                try
                {
                    explodes[i].Draw();
                }
                catch
                {
                    explodes.RemoveAt(i);
                }

            }
            if (currentPrize != null)
                currentPrize.Draw();
        }

        private void UpdateGame()
        {
            if (isWdown && firstPlayer.GetCollider().IntersectsWith(screenCollider)) // ?
                firstPlayer.Update(new Vector2(0f, 0.01f));
            if (isSdown)
                firstPlayer.Update(new Vector2(0f, -0.01f));
            if (isIdown && secondPlayer.GetCollider().IntersectsWith(screenCollider))
                secondPlayer.Update(new Vector2(0f, 0.01f));
            if(isKdown)
                secondPlayer.Update(new Vector2(0f, -0.01f));
           /* if(isJdown)
            {
                
            }*/

            if (landCollider.IntersectsWith(firstPlayer.GetCollider()) || !firstPlayer.CheckAlive())
            {
                glTimer.Stop();
                glTimer.Tick -= glTimer_Tick;
                explodes.Add(new Explode(firstPlayer.GetPosition()));
                glTimer.Tick += glTimer_FirstPlayerLooseTick;
                glTimer.Start();
                return;
                //glTimer.Stop();
                //MessageBox.Show("GAME IS OVER! FIRST PLAYER IS LOSED.");
                
                //this.Close();
            }
            if (landCollider.IntersectsWith(secondPlayer.GetCollider()) || !secondPlayer.CheckAlive())
            {
                glTimer.Stop();
                glTimer.Tick -= glTimer_Tick;
                explodes.Add(new Explode(secondPlayer.GetPosition()));
                glTimer.Tick += glTimer_SecondPlayerLooseTick;
                glTimer.Start();
                return;
            }

            for (int i=0; i < firstAmmos.Count; i++)
            {
                firstAmmos[i].Update();
                if (secondPlayer.GetCollider().IntersectsWith(firstAmmos[i].GetCollider()))
                {
                    explodes.Add(new Explode(firstAmmos[i].GetPosition()));
                    firstAmmos.RemoveAt(i);
                    secondPlayer.GetDamage();
                }
                else if (!firstAmmos[i].GetCollider().IntersectsWith(screenCollider))
                {
                    firstAmmos.RemoveAt(i);
                }
            }

            for (int i = 0; i < secondAmmos.Count; i++)
            {
                secondAmmos[i].Update();
                if (firstPlayer.GetCollider().IntersectsWith(secondAmmos[i].GetCollider()))
                {
                    explodes.Add(new Explode(secondAmmos[i].GetPosition()));
                    secondAmmos.RemoveAt(i);
                    firstPlayer.GetDamage();
                }
                else if(!secondAmmos[i].GetCollider().IntersectsWith(screenCollider))
                {
                    secondAmmos.RemoveAt(i);
                }
            }
            
            if(currentPrize!=null)
            {
                currentPrize.Update();
                if (!screenCollider.IntersectsWith(currentPrize.GetCollider()))
                {
                    currentPrize = null;
                }
            }
                

            
            firstPlayer.Update();
            secondPlayer.Update();
            //firstPlayer.Update();
        }

        private void glTimer_FirstPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            for (int i = 0; i < explodes.Count; i++)
            {
                try
                {
                    Debug.WriteLine($"Explodes.Count = {explodes[i].Count}");

                    explodes[i].Draw();
                }
                catch
                {
                    explodes.RemoveAt(i);
                }

            }
            
            if (explodes.Count<=0)
            {
                glTimer.Stop();
                MessageBox.Show("GAME IS OVER! FIRST PLAYER IS LOSED.");

                this.Close();
            }
        }

        

        private void glTimer_SecondPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            for (int i = 0; i < explodes.Count; i++)
            {
                try
                {
                    explodes[i].Draw();
                }
                catch
                {
                    explodes.RemoveAt(i);
                }

            }
            if (explodes.Count <= 0)
            {
                glTimer.Stop();
                MessageBox.Show("GAME IS OVER! SECOND PLAYER IS LOSED.");

                this.Close();
            }
        }

        private void glTimer_Tick(object sender, EventArgs e) // для обновления картинки каждые 16 миллисекунд (чуть больше 60 фреймов в секунде)
        {
            //     firstPlayer.PositionCenter += new Vector2(0.5f, 0.5f);

            //Prize prize = new FuelPrize();

            //if (prize is FuelPrize)
            //    Debug.WriteLine("prize is FuelPrize");
            
            UpdateGame();

            glControl.Refresh();
        }

        private void prizeTimer_Tick(object sender, EventArgs e)
        {
            if (currentPrize != null)
                return;

            Prize newPrize = null;
            PrizeGenerator prizeGenerator=null;
            float prizePozitionX;
            bool isLeft; // переменная, отвечающая за направление движения

            int prizeSpawnSide = random.Next(0, 2); // 0 - спавнится слева, 1 - справа



            if (prizeSpawnSide == 0)
            {
                isLeft = false;
                prizePozitionX = -1.05f;
            }
                
            else
            {
                isLeft = true;
                prizePozitionX = 1.05f;
            }

            float prizePozitionY = (float)(random.Next((int)(0.1 * Height), (int)(0.7 * Height))) / (float)Height; // спавн в пределах экрана от 10% сверху и 30% снизу

            int prizeType = random.Next(0, 4);

            switch(prizeType)
            {
                case 0:
                    prizeGenerator = new AmmoGenerator();
                    break;
                case 1:
                    prizeGenerator = new ArmourGenerator();
                    break;
                case 2:
                    prizeGenerator = new FuelGenerator();
                    break;
                case 3:
                    prizeGenerator = new HealthGenerator();
                    break;
            }
            if(prizeGenerator != null)
            {
                newPrize = prizeGenerator.Create(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                currentPrize = newPrize;
            }
                
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            glControl.Size = this.Size;
            GL.Viewport(0, 0, Width, Height);
        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        isWdown = false;
                    //    firstPlayer.Update(new Vector2(0f, 0.01f));
                    //    Debug.WriteLine("W");
                        break;
                    }
                case Keys.S:
                    {
                        isSdown = false;
                   //     firstPlayer.Update(new Vector2(0f, -0.01f));
                        break;
                    }
                case Keys.I:
                    {
                        isIdown = false;
                   //     secondPlayer.Update(new Vector2(0f, 0.01f));
                   //     Debug.WriteLine("UP ARROW");
                        break;
                    }
                case Keys.K:
                    {
                        isKdown = false;
                     //   secondPlayer.Update(new Vector2(0f, -0.01f));
                        break;
                    }
                case Keys.J:
                    {
                        secondAmmos.Add(new SupersonicAmmo(secondPlayer.PositionCenter, true, TextureDrawer.LoadTexure("supersonicAmmo.png")));
                        isJdown = false;
                        break;
                    }
                case Keys.D:
                    {
                        firstAmmos.Add(new SupersonicAmmo(firstPlayer.PositionCenter, false, TextureDrawer.LoadTexure("supersonicAmmo_2.png")));
                        isDdown = false;
                        break;
                    }
            }
        }
        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        isWdown = true;
                        break;
                    }
                case Keys.S:
                    {
                        isSdown = true;
                        break;
                    }
                case Keys.I:
                    {
                        isIdown = true;
                        break;
                    }
                case Keys.K:
                    {
                        isKdown = true;
                        break;
                    }
                case Keys.J:
                    {
                        
                        isJdown = true;
                        break;
                    }
                case Keys.D:
                    {
                        isDdown = true;
                        break;
                    }
            }
        }
    }

        
}
