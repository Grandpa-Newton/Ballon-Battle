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
using PrizesLibrary.Generators;
using GraphicsOpenGL;

namespace Ballon_Battle
{
    public partial class GameForm : Form
    {
        Texture backgroundTexture; // текстура фона
        Texture landTexture; // текстура земли
        Balloon firstPlayer; // объект первого игрока
        Balloon secondPlayer; // объект второго игрока
        RectangleF landCollider; // границы земли
        RectangleF screenCollider; // границы экрана
        RectangleF firstPlayerCollider; // коллайдер первого игрока
        RectangleF secondPlayerCollider;
        RectangleF currentPrizeCollider; // коллайдер для текущего приза
        List<Ammo> firstAmmos; // текущие снаряды первого игрока
        List<Ammo> secondAmmos; // текущие снаряды второго игрока
        List<Explode> explodes; // анимации взрывов
        Random random = new Random();
        Prize currentPrize = null; // объект текущего приза
        Label firstPlayerInfo; // label для отображения текущего состояния первого игрока
        Label secondPlayerInfo; // label для отображения текущего состояния второго игрока
        int maxWindSpeed = 20; // максимальная возможнная скорость ветра (умноженная на 10000)
        int minWindSpeed = 5; // минимальная скорость ветра
        int windTicks = 0; // количество тиков таймера ветра
        bool isFirstPlayerWindLeft = false; // true - ветер дует налево, false - направо
        bool isSecondPlayerWindLeft = false;

        List<bool> keysDown; // список для проверки нажатия кнопок (W, S, I, K, J, D, A, L)

        int secondPlayerTicks = 50; // показатель, отвечающий за кулдаун снарядов второго игрока
        int firstPlayerTicks = 50;
        
        public GameForm()
        {
            
            InitializeComponent();
            CenterToScreen();
            glControl.Size = this.Size;
            glControl.Visible = true;
            glTimer.Start();
            prizeTimer.Start();
            windTimer.Start();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();

        /*    GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend); // для отключения фона у ассетов
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);*/
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            LoadObjects();

            glControl.SendToBack();

            this.WindowState = FormWindowState.Maximized; // для открытия окна в полном экране
        }

        private void LoadObjects()
        {
            backgroundTexture = TextureLoader.LoadTexure("clouds.jpg");

            Texture firstPlayerTexture = TextureLoader.LoadTexure("firstPlayerBalloon.png");

            firstPlayer = new Balloon(new Vector2(-0.7f, 0.0f), firstPlayerTexture);

            Texture secondPlayerTexture = TextureLoader.LoadTexure("testBalloon_2.png");

            secondPlayer = new Balloon(new Vector2(0.7f, 0.0f), secondPlayerTexture);

            landTexture = TextureLoader.LoadTexure("grasstexture_new.png");

            firstAmmos = new List<Ammo>();

            secondAmmos = new List<Ammo>();

            screenCollider = new RectangleF(0.0f, 0.0f, 1.0f, 0.875f);

            landCollider = new RectangleF(0.0f, 0.875f, 1.0f, 0.125f);

            explodes = new List<Explode>();

            keysDown = new List<bool>();

            for (int i = 0; i < 8; i++)
                keysDown.Add(false);
        }
        
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            Draw();

            glControl.SwapBuffers();
        }

        private void Draw()
        {

            // отрисовка фона

            ObjectDrawer.Draw(backgroundTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            }, false);

            // отрисовка земли

            ObjectDrawer.Draw(landTexture, new Vector2[4]
            {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, -0.75f),
                new Vector2(-1.0f, -0.75f),
            }, false);
            
            // отрисовка игроков

            firstPlayer.Draw(false);
            secondPlayer.Draw(false);

            // отрисовка снарядов

            foreach (var item in firstAmmos)
            {
                item.Draw();
            }
            foreach (var item in secondAmmos)
            {
                item.Draw();
            }

            // отрисовка взрывов

            for (int i = 0; i < explodes.Count; i++)
            {
                int thisCount = explodes.Count;
                
                if(!explodes[i].Draw(false))
                {
                    explodes.RemoveAt(i);
                }
                
                if (thisCount != explodes.Count)
                    i--;

            }
            if (currentPrize != null)
                currentPrize.Draw(false);
        }

        private void UpdateInput()
        {
            if (keysDown[0] && (firstPlayerCollider.Y > screenCollider.Y))
                firstPlayer.Update(new Vector2(0f, 0.01f));
            if (keysDown[1])
                firstPlayer.Update(new Vector2(0f, -0.01f));
            if (keysDown[2] && (secondPlayerCollider.Y > screenCollider.Y))
                secondPlayer.Update(new Vector2(0f, 0.01f));
            if (keysDown[3])
                secondPlayer.Update(new Vector2(0f, -0.01f));
            if ((keysDown[4] || keysDown[7]) && secondPlayerTicks >= 50)
            {
                secondPlayerTicks = 0;
                Ammo newAmmo = null;
                if (keysDown[4])
                    newAmmo = secondPlayer.GetCurrentAmmo(true);
                else if (keysDown[7])
                    newAmmo = secondPlayer.GetCurrentAmmo(false);
                secondAmmos.Add(newAmmo);
            }
            if ((keysDown[5] || keysDown[6]) && firstPlayerTicks >= 50)
            {
                firstPlayerTicks = 0;
                Ammo newAmmo = null;
                if (keysDown[5])
                    newAmmo = firstPlayer.GetCurrentAmmo(false);
                else if (keysDown[6])
                    newAmmo = firstPlayer.GetCurrentAmmo(true);
                firstAmmos.Add(newAmmo);

            }
        }

        private void CheckCollisions()
        {
            if ((firstPlayerCollider.X <= screenCollider.X) && isFirstPlayerWindLeft) // ?
            {
                firstPlayer.ChangeWindCondition(false);
            }
            else if ((firstPlayerCollider.X + secondPlayerCollider.Width >= screenCollider.X + screenCollider.Width) && !isFirstPlayerWindLeft)
            {
                firstPlayer.ChangeWindCondition(false);
            }
            else
                firstPlayer.ChangeWindCondition(true);
            if ((secondPlayerCollider.X + secondPlayerCollider.Width >= screenCollider.X + screenCollider.Width) && !isSecondPlayerWindLeft) // || 
            {
                secondPlayer.ChangeWindCondition(false);
            }
            else if ((secondPlayerCollider.X <= screenCollider.X) && isSecondPlayerWindLeft)
            {
                secondPlayer.ChangeWindCondition(false);
            }
            else
                secondPlayer.ChangeWindCondition(true);
            if (landCollider.IntersectsWith(firstPlayerCollider) || !firstPlayer.CheckAlive())
            {
                glTimer.Stop();
                prizeTimer.Stop();
                windTimer.Stop();
                glTimer.Tick -= glTimer_Tick;
                explodes.Add(new Explode(firstPlayer.GetPosition()));
                glTimer.Tick += glTimer_FirstPlayerLooseTick;
                glTimer.Start();
                return;
                //glTimer.Stop();
                //MessageBox.Show("GAME IS OVER! FIRST PLAYER IS LOSED.");

                //this.Close();
            }
            if (landCollider.IntersectsWith(secondPlayerCollider) || !secondPlayer.CheckAlive())
            {
                glTimer.Stop();
                prizeTimer.Stop();
                windTimer.Stop();
                glTimer.Tick -= glTimer_Tick;
                explodes.Add(new Explode(secondPlayer.GetPosition()));
                glTimer.Tick += glTimer_SecondPlayerLooseTick;
                glTimer.Start();
                return;
            }
            if (firstPlayerCollider.IntersectsWith(secondPlayerCollider))
            {
                glTimer.Stop();
                prizeTimer.Stop();
                windTimer.Stop();
                glTimer.Tick -= glTimer_Tick;
                explodes.Add(new Explode(firstPlayer.GetPosition()));
                explodes.Add(new Explode(secondPlayer.GetPosition()));
                glTimer.Tick += glTimer_DrawTick;
                glTimer.Start();
                return;
            }
        }

        private void UpdateAmmos()
        {
            for (int i = 0; i < firstAmmos.Count; i++)
            {
                int thisCount = firstAmmos.Count;
                RectangleF ammoCollider = firstAmmos[i].GetCollider(false);
                firstAmmos[i].Update();
                if (secondPlayerCollider.IntersectsWith(ammoCollider))
                {
                    firstAmmos[i].UpdatePosition(true); // ?!!
                    explodes.Add(new Explode(firstAmmos[i].Position));
                    firstAmmos.RemoveAt(i);
                    secondPlayer.GetDamage();
                }
                else if (!ammoCollider.IntersectsWith(screenCollider)) // ВЫХОД ЗА РАМКИ МАССИВА
                {
                    firstAmmos.RemoveAt(i);
                }
                else if (firstAmmos[i].Distance <= 0)
                {
                    RectangleF ammoExplode = firstAmmos[i].GetCollider(true);
                    explodes.Add(new Explode(firstAmmos[i].Position));
                    firstAmmos.RemoveAt(i);
                    if (ammoExplode.IntersectsWith(secondPlayerCollider))
                    {
                        secondPlayer.GetDamage();
                    }
                }
                if (thisCount != firstAmmos.Count)
                    i--;
            }

            for (int i = 0; i < secondAmmos.Count; i++)
            {
                int thisCount = secondAmmos.Count;
                RectangleF ammoCollider = secondAmmos[i].GetCollider(false);
                secondAmmos[i].Update();
                if (firstPlayerCollider.IntersectsWith(ammoCollider))
                {
                    secondAmmos[i].UpdatePosition(true);
                    explodes.Add(new Explode(secondAmmos[i].Position));
                    secondAmmos.RemoveAt(i);
                    firstPlayer.GetDamage();
                }
                else if (!ammoCollider.IntersectsWith(screenCollider)) // ВЫХОД ЗА РАМКИ МАССИВА
                {
                    secondAmmos.RemoveAt(i);
                }
                else if (secondAmmos[i].Distance <= 0)
                {
                    RectangleF ammoExplode = secondAmmos[i].GetCollider(true);
                    explodes.Add(new Explode(secondAmmos[i].Position));
                    secondAmmos.RemoveAt(i);
                    if (ammoExplode.IntersectsWith(firstPlayerCollider))
                    {

                        firstPlayer.GetDamage();
                    }
                }
                if (thisCount != secondAmmos.Count)
                    i--;
            }
        }

        private void UpdatePrize()
        {
            if (currentPrize != null)
            {
                currentPrizeCollider = currentPrize.GetCollider();
                currentPrize.Update();
                if (firstPlayerCollider.IntersectsWith(currentPrizeCollider))
                {
                    if (currentPrize is AmmoPrize)
                    {
                        Debug.WriteLine("TESTAMMO");
                        int decoratorType = random.Next(0, 3);
                        firstPlayer.ChangeAmmoCharesterictics(decoratorType); // БУДЕТ RAND
                        currentPrize = null;
                    }
                    else if (currentPrize is ArmourPrize)
                    {
                        Debug.WriteLine("TESTARMOUR");
                        firstPlayer.IncreaseArmour();
                        currentPrize = null;
                    }
                    else if (currentPrize is FuelPrize)
                    {
                        Debug.WriteLine("TESTFUEL");
                        firstPlayer.IncreaseFuel();
                        currentPrize = null;
                    }
                    else if (currentPrize is HealthPrize)
                    {
                        Debug.WriteLine("TESTHEALTH");
                        firstPlayer.IncreaseHealth();
                        currentPrize = null;
                    }
                }
                if (currentPrize != null && secondPlayerCollider.IntersectsWith(currentPrizeCollider))
                {
                    if (currentPrize is AmmoPrize)
                    {
                        int decoratorType = random.Next(0, 3);
                        secondPlayer.ChangeAmmoCharesterictics(decoratorType); // БУДЕТ RAND
                        currentPrize = null;
                    }
                    else if (currentPrize is ArmourPrize)
                    {
                        secondPlayer.IncreaseArmour();
                        currentPrize = null;
                    }
                    else if (currentPrize is FuelPrize)
                    {
                        secondPlayer.IncreaseFuel();
                        currentPrize = null;
                    }
                    else if (currentPrize is HealthPrize)
                    {
                        secondPlayer.IncreaseHealth();
                        currentPrize = null;
                    }
                }
                if (currentPrize != null && !screenCollider.IntersectsWith(currentPrizeCollider))
                {
                    currentPrize = null;
                }

            }
        }
        
        private void UpdateInfo()
        {
            firstPlayerInfo.SetBounds((int)(0.1 * Width), (int)(0.01 * Height), (int)(0.24 * Width), (int)(0.03 * Height)); // информация первого игрока (здоровье, топливо, броня)
            firstPlayerInfo.Font = new Font("Arial", 0.01f * Width);
            firstPlayerInfo.Text = $"Health = {firstPlayer.Health}, Armour = {firstPlayer.Armour}, Fuel = {firstPlayer.Fuel}";

            secondPlayerInfo.SetBounds((int)(0.7 * Width), (int)(0.01 * Height), (int)(0.24 * Width), (int)(0.03 * Height)); // информация первого игрока (здоровье, топливо, броня)
            secondPlayerInfo.Font = new Font("Arial", 0.01f * Width);
            secondPlayerInfo.Text = $"Health = {secondPlayer.Health}, Armour = {secondPlayer.Armour}, Fuel = {secondPlayer.Fuel}";
        }

        private void UpdateGame()
        {
            firstPlayerTicks++;
            secondPlayerTicks++;

            firstPlayer.Update();
            secondPlayer.Update();

            firstPlayerCollider = firstPlayer.GetCollider();
            secondPlayerCollider = secondPlayer.GetCollider();

            UpdateInput();
            CheckCollisions();
            UpdateAmmos();
            UpdatePrize();
            UpdateInfo();
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

            if (explodes.Count <= 0)
            {
                EndGame("ИГРА ОКОНЧЕНА! НИЧЬЯ! Хотите начать заново?");
            }
        }
        private void glTimer_FirstPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            if (explodes.Count<=0)
            {
                EndGame("ИГРА ОКОНЧЕНА! ПЕРВОЙ ИГРОК ПРОИГРАЛ. Хотите начать заново?");
            }
        }
        private void glTimer_SecondPlayerLooseTick(object sender, EventArgs e)
        {
            glControl.Refresh();
            if (explodes.Count <= 0)
            {
                EndGame("ИГРА ОКОНЧЕНА! ВТОРОЙ ИГРОК ПРОИГРАЛ. Хотите начать заново?");
            }
        }

        private void windTimer_Tick(object sender, EventArgs e)
        {
            if(windTicks == 1)
            {
                firstPlayer.ChangeWindCondition(false);
                secondPlayer.ChangeWindCondition(false);
            }
            else
            {
                int windDirection = random.Next(0, 2); // 0 - влево, 1 - вправо
                float windSpeed = random.Next(minWindSpeed, maxWindSpeed+1) / 10000f;
                
                switch(windDirection)
                {
                    case 0:
                        firstPlayer.ChangeWindSpeed(new Vector2(-windSpeed, 0.0f));
                        isFirstPlayerWindLeft = true;
                        break;
                    case 1:
                    
                        firstPlayer.ChangeWindSpeed(new Vector2(windSpeed, 0.0f));
                        isFirstPlayerWindLeft = false;
                        break;
                }
                firstPlayer.ChangeWindCondition(true);

                windDirection = random.Next(0, 2); // 0 - влево, 1 - вправо
                windSpeed = random.Next(minWindSpeed, maxWindSpeed+1) / 10000f;

                switch (windDirection)
                {
                    case 0:
                        
                        secondPlayer.ChangeWindSpeed(new Vector2(-windSpeed, 0.0f));
                        isSecondPlayerWindLeft = true;
                        break;
                    case 1:
                        
                        secondPlayer.ChangeWindSpeed(new Vector2(windSpeed, 0.0f));
                        isSecondPlayerWindLeft = false;
                        break;
                }
                secondPlayer.ChangeWindCondition(true);
            }
            windTicks++;
            if (windTicks >= 2)
                windTicks = 0;
        }

        private void glTimer_Tick(object sender, EventArgs e) // для обновления картинки каждые 10 миллисекунд (100 фреймов в секунду)
        {   
            UpdateGame();

            glControl.Refresh();
        }

        private void prizeTimer_Tick(object sender, EventArgs e)
        {
            if (currentPrize != null) // если на экране уже есть приз, то новый не должен спавниться
                return;

            Prize newPrize;
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

            float prizePozitionY = (float)(random.Next((int)(-0.6f * Height), (int)(0.7f * Height))) / (float)Height; // спавн в пределах экрана по Y (-0.6;0.7)

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
                        keysDown[0] = false;
                        break;
                    }
                case Keys.S:
                    {
                        keysDown[1] = false;
                        break;
                    }
                case Keys.I:
                    {
                        keysDown[2] = false;
                        break;
                    }
                case Keys.K:
                    {
                        keysDown[3] = false;
                        break;
                    }
                case Keys.J:
                    {
                        keysDown[4] = false;
                        break;
                    }
                case Keys.D:
                    {
                        keysDown[5] = false;
                        break;
                    }
                case Keys.A:
                    {
                        keysDown[6] = false;
                        break;
                    }
                case Keys.L:
                    {
                        keysDown[7] = false;
                        break;
                    }
                case Keys.M:
                    {
                        secondPlayer.ChangeAmmo();
                        break;
                    }
                case Keys.X:
                    {
                        firstPlayer.ChangeAmmo();
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
                        keysDown[0] = true; 
                        break;
                    }
                case Keys.S:
                    {
                        keysDown[1] = true;
                        break;
                    }
                case Keys.I:
                    {
                        keysDown[2] = true;
                        break;
                    }
                case Keys.K:
                    {
                        keysDown[3] = true;
                        break;
                    }
                case Keys.J:
                    {
                        keysDown[4] = true;
                        break;
                    }
                case Keys.D:
                    {
                        keysDown[5] = true;
                        break;
                    }
                case Keys.A:
                    {
                        keysDown[6] = true;
                        break;
                    }
                case Keys.L:
                    {
                        keysDown[7] = true;
                        break;
                    }
            }
        }
    }
}
