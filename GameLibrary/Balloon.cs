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
using AmmoLibrary;
using AmmoLibrary.characteristics_changing;

namespace GameLibrary
{
    public class Balloon
    {
        public Vector2 PositionCenter;
        public Vector2 Speed;
        public Texture BalloonSprite;
    //    private bool isMoving = false; // переменная для проверки на то, двигает ли игрок воздушный шар
        readonly List<Ammo> ammos; // список с видами снарядов
        int currentAmmo=0; // показатель, отвечающий за то, какой сейчас снаряд у игрока
        Vector2 windSpeed = new Vector2(0.0f, 0.0f); // скорость ветра
        bool isWindOn = false; // работает ли ветер
        
        public Balloon(Vector2 startPosition, Texture baloonSprite)
        {
            this.PositionCenter = startPosition;
            this.BalloonSprite = baloonSprite;
            this.Speed = new Vector2(0, -0.001f); // изначально скорость нулевая
            this.currentAmmo = 0;
            this.ammos = new List<Ammo>()
            {
                new SupersonicAmmo(TextureLoader.LoadTexure("supersonicAmmo.png")),
                new PiercingAmmo(TextureLoader.LoadTexure("piercingAmmo.png")),
                new ExplosiveAmmo(TextureLoader.LoadTexure("explosiveAmmo.png")),
            };
        }
        public int Armour { get; set; } = 0;
        public int Health { get; set; } = 100;
        public int Fuel { get; set; } = 1000; // по таймеру отнимается каждый кадр
        public bool CheckAlive()
        {
            if (Health <= 0)
                return false;
            else
                return true;
        }

        public void Update(Vector2 movement)
        {
            if (Fuel <= 0)
                return;
            PositionCenter += movement;
            Fuel--;
            if (isWindOn)
                PositionCenter += windSpeed;
        }

        public void Update() // обновление падения вниз
        {
            PositionCenter += Speed;
            if(isWindOn)
                PositionCenter += windSpeed;
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
                    Armour = 0;
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
            int extraFuel = 350;
            Fuel += extraFuel;
            if (Fuel > 700)
                Fuel = 700;
        }

        public void ChangeWindSpeed(Vector2 windSpeed)
        {
            this.windSpeed = windSpeed; 
        }

        public void ChangeWindCondition(bool isWindOn)
        {
            this.isWindOn = isWindOn;
        }

        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition();

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X)/2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y)/2.0f;

            float[] convertedLeftTop = CoordinatesConverter.Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }

        public void Draw(bool isFlipped)
        {
            ObjectDrawer.Draw(BalloonSprite, GetPosition(), isFlipped);
        }

        public void ChangeAmmo()
        {
            currentAmmo++;
            if (currentAmmo >= ammos.Count)
                currentAmmo = 0;
        }

        public Ammo GetCurrentAmmo(bool isLeft)
        {
            Ammo newAmmo = null;
            ammos[currentAmmo].Spawn(PositionCenter-new Vector2(0.01f, 0.07f), isLeft); // отнимаем вектор для выпуска снарядов из корзины шара, а не из центра шара
            switch(currentAmmo)
            {
                case 0:
                    newAmmo = new SupersonicAmmo(ammos[currentAmmo]);
                    break;
                case 1:
                    newAmmo = new PiercingAmmo(ammos[currentAmmo]);
                    break;
                case 2:
                    newAmmo = new ExplosiveAmmo(ammos[currentAmmo]);
                    break;
            }
            
            return newAmmo;
        }

        public void ChangeAmmoCharesterictics(int decoratorType)
        {
            switch(decoratorType)
            {
                case 0:
                    for (int i = 0; i < ammos.Count; i++)
                    {
                        ammos[i] = new DistanceDecorator(ammos[i]);
                        Debug.WriteLine("Distance Decorator");
                    }
                    break;
                case 1:
                    for (int i = 0; i < ammos.Count; i++)
                    {
                        ammos[i] = new RadiusDecorator(ammos[i]);

                        Debug.WriteLine("Radius Decorator");
                    }
                    break;
                case 2:
                    for (int i = 0; i < ammos.Count; i++)
                    {
                        ammos[i] = new SpeedDecorator(ammos[i]);

                        Debug.WriteLine("Speed Decorator");
                    }
                    break;
            }
        }

        public Vector2[] GetPosition()
        {
            float spriteWidth = 0.07f;
            float spriteHeight = 0.14f;
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, -spriteHeight),
                PositionCenter + new Vector2(spriteWidth, spriteHeight),
                PositionCenter + new Vector2(-spriteWidth, spriteHeight),
            };
        }
    }
}
