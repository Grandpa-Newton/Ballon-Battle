using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;
using System;

namespace UnitTests
{
    [TestClass]
    public class CheckAliveTest
    {
        [TestMethod]
        public void CheckAliveTestMethod1()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            balloon.Health = 0;
            bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = balloon.CheckAlive();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void IncreaseHealthTestMethod2()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            balloon.Health = 2;
            bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = balloon.CheckAlive();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

