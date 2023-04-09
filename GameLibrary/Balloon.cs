using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using System.Drawing;
using OpenTK.Input;
using Ballon_Battle;
using System.Media;
using System.Diagnostics;

namespace GameLibrary
{
    public class Balloon
    {
        public Vector2 PositionCenter;
        public Vector2 Speed;
        public Texture BalloonSprite;
        private bool isMoving = false; // переменная для проверки на то, двигает ли игрок воздушный шар

        public Balloon(Vector2 startPosition, Texture baloonSprite)
        {
            this.PositionCenter = startPosition;
            this.BalloonSprite = baloonSprite;
            this.Speed = new Vector2(0, -0.001f); // изначально скорость нулевая
        }
        public int Armour { get; set; } = 0;
        public int Health { get; set; } = 100;
        public int Fuel { get; set; } = 5000; // по таймеру отнимается каждый кадр
        public bool CheckAlive()
        {
            if (Health <= 0)
                return false;
            else
                return true;
             
        }

        public void Update(Vector2 movement)
        {
            if (isMoving)
                return;
            PositionCenter += movement;
            Fuel--;

            Debug.WriteLine("Fuel is " + Fuel);
        }

        public void Update() // обновление падения вниз
        {
            isMoving = true;
            PositionCenter += Speed;
            isMoving = false;
        }

        public void Input()
        {
        }

        public void Draw()
        {
            ObjectsDrawing.Start();

            ObjectsDrawing.Draw(BalloonSprite, new Vector2[4]
            {
                PositionCenter + new Vector2(-0.1f, -0.2f),
                PositionCenter + new Vector2(0.1f, -0.2f),
                PositionCenter + new Vector2(0.1f, 0.2f),
                PositionCenter + new Vector2(-0.1f, 0.2f),
            });
        }
    }
}
