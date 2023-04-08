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

namespace GameLibrary
{
    public class Balloon
    {
        public Vector2 PositionCenter;
        public Vector2 Speed;
        public Texture BalloonSprite;

        public Balloon(Vector2 startPosition, Texture baloonSprite)
        {
            this.PositionCenter = startPosition;
            this.BalloonSprite = baloonSprite;
            this.Speed = Vector2.Zero; // изначально скорость нулевая
        }
        public int Armour { get; set; } = 0;
        public int Health { get; set; } = 100;
        public int Fuel { get; set; } = 50; // по таймеру отнимается каждый кадр
        public bool CheckAlive()
        {
            if (Health <= 0)
                return false;
            else
                return true;
             
        }

        public void Update(KeyEventArgs e)
        {
            Input();

            Speed += new Vector2(0, 0.1f);
            PositionCenter += Speed;
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
