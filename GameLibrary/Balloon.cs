using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using System.Drawing;
using OpenTK.Input;
using System.Media;
using System.Diagnostics;
using GraphicsOpenGL;

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
        public int Fuel { get; set; } = 700; // по таймеру отнимается каждый кадр
        public bool CheckAlive()
        {
            if (Health <= 0)
                return false;
            else
                return true;
             
        }

        public void Update(Vector2 movement)
        {
            if (isMoving || Fuel <= 0)
                return;
            PositionCenter += movement;
            Fuel--;

            Debug.WriteLine("Fuel is " + Fuel);
        }

        public void Update() // обновление падения вниз
        {
            isMoving = true; // !!! возможно убрать!
            PositionCenter += Speed;
            isMoving = false;
        }

        public void GetDamage() // получение дамага после удара
        {
            int damage = 15;
            if(Armour>0)
            {
                if(Armour>damage)
                {
                    Armour -= damage;
                }
                else
                {
                    int remainder = damage - Armour; // если брони меньше, чем урона, то сначала сносятся пункты брони до нуля, после - остаток со здоровья.
                    Health -= remainder;
                }
            }
            else
                Health -= damage;
            if (Health < 0)
                Health = 0;
        }

        public void IncreaseHealth()
        {
            int extraHealth = 20;
            Health += extraHealth;
            if (Health > 100)
                Health = 100;
        }

        public void IncreaseArmour()
        {
            int extraArmour = 20;
            Armour += extraArmour;
            if (Armour > 100)
                Armour = 100;
        }

        public void IncreaseFuel()
        {
            int extraFuel = 200;
            Fuel += extraFuel;
            if (Fuel > 700)
                Fuel = 700;
        }



        public void Input() // ??

        {
        }

        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition(); // добавить более точную коллизию!

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X)/2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y)/2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        public void Draw()
        {
            ObjectsDrawing.Draw(BalloonSprite, GetPosition());
        }

        public Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-0.1f, -0.2f),
                PositionCenter + new Vector2(0.1f, -0.2f),
                PositionCenter + new Vector2(0.1f, 0.2f),
                PositionCenter + new Vector2(-0.1f, 0.2f),
            };
        }
    }
}
