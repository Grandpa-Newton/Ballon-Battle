using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;
using System;

namespace UnitTests
{
    [TestClass]
    public class IncreaseHealthTest
    {
        [TestMethod]
        public void IncreaseHealthTestMethod1()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            balloon.Health = 50;
            int expectedHealth = 70;
            int actualHealth;

            // Act
            balloon.IncreaseHealth();
            actualHealth = balloon.Health;

            // Assert
            Assert.AreEqual(expectedHealth, actualHealth);
        }
        [TestMethod]
        public void IncreaseHealthTestMethod2()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            balloon.Health = 90;
            int expectedHealth = 100;
            int actualHealth;

            // Act
            balloon.IncreaseHealth();
            actualHealth = balloon.Health;

            // Assert
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}
