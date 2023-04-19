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
            float centralPointX = 0.5f; // значения (0,0) в OpenGL и WinForms не совпадают
            float centralPointY = 0.5f; // в Winforms - левый верхний гол, OpenGL - центр

            float[] resultPoint = new float[2];

            resultPoint[0] = centralPointX + pointX/2.0f;
            resultPoint[1] = centralPointY - pointY/2.0f;

            return resultPoint;
        }
    }
}
