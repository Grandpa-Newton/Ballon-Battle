using AmmoLibrary;
using GameLibrary;
using GraphicsOpenGL;
using OpenTK;
using PrizesLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Ballon_Battle
{
    public class BattleGame
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
        int maxWindSpeed = 20; // максимальная возможнная скорость ветра (умноженная на 10000)
        int minWindSpeed = 5; // минимальная скорость ветра
        int windTicks = 0; // количество тиков таймера ветра
        bool isFirstPlayerWindLeft = false; // true - ветер дует налево, false - направо
        bool isSecondPlayerWindLeft = false;

        List<bool> keysDown; // список для проверки нажатия кнопок (W, S, I, K, J, D, A, L)

        int secondPlayerTicks = 50; // показатель, отвечающий за кулдаун снарядов второго игрока
        int firstPlayerTicks = 50;

        private void LoadObjects()
        {

        }

        public void LoadGLControl()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend); // для отключения фона у ассетов
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            

            LoadObjects();
        }
    }
}
