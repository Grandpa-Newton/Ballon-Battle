using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsOpenGL
{
    public static class CoordinatesConverter // класс, предназначенный для конвертации из системы координат OpenGL в WinForms
    {
        public static float[] Convert(float pointX, float pointY)
        {
            /*   float centralPointX = 0.5f; 
               float centralPointY = 0.5f; // в Winforms - левый верхний гол, OpenGL - центр

               float[] resultPoint = new float[2];

               resultPoint[0] = centralPointX + pointX/2.0f;
               resultPoint[1] = centralPointY - pointY/2.0f;

               return resultPoint;*/

            decimal centralPointX = 0.5M; // значения (0,0) в OpenGL и WinForms не совпадают
            decimal centralPointY = 0.5M; // в Winforms - левый верхний гол, OpenGL - центр

            // использование decimal для более высокой точности вычислений

            float[] resultPoint = new float[2];

            resultPoint[0] = (float)(centralPointX + (decimal)(pointX / 2.0f));
            resultPoint[1] = (float)(centralPointY - (decimal)(pointY / 2.0f));
            
            return resultPoint;
        }
    }
}
