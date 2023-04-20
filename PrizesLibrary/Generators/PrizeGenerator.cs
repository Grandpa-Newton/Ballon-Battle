using GraphicsOpenGL;
using OpenTK;
using PrizesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary
{
/*    public abstract class PrizeGenerator
    {
        protected Texture sprite;

        public abstract Prize Create(Vector2 centerPosition, bool isLeft);


    }*/

    public class PrizeGenerator
    {
        public Prize Create(int width, int height)
        {
            Random random = new Random();
            Prize newPrize=null;
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

            float prizePozitionY = (float)(random.Next((int)(-0.6f * height), (int)(0.7f * height))) / (float)height; // спавн в пределах экрана по Y (-0.6;0.7)

            int prizeType = random.Next(0, 4);

            switch (prizeType)
            {
                case 0:
                    newPrize = new AmmoPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
                case 1:
                    newPrize = new ArmourPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
                case 2:
                    newPrize = new FuelPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
                case 3:
                    newPrize = new HealthPrize(new Vector2(prizePozitionX, prizePozitionY), isLeft);
                    break;
            }

            return newPrize;
        }
    }
}