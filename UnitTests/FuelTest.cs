using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;
using System;

namespace UnitTests
{
    [TestClass]
    public class FuelTest
    {
        [TestMethod]
        public void IncreaseFuelTestMethod1()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            balloon.Fuel = 200;
            int expectedFuel = 550;
            int actualFuel;

            // Act
            balloon.IncreaseFuel();
            actualFuel = balloon.Fuel;

            // Assert
            Assert.AreEqual(expectedFuel, actualFuel);
        }
        [TestMethod]
        public void IncreaseFuelTestMethod2()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            game.LoadGLControl();
            Balloon balloon = new Balloon(Vector2.Zero, null); // отрисовка и позиция для проверки нам не требуется
            balloon.Fuel = 800;
            int expectedFuel = 1000;
            int actualFuel;

            // Act
            balloon.IncreaseFuel();
            actualFuel = balloon.Fuel;

            // Assert
            Assert.AreEqual(expectedFuel, actualFuel);
        }
    }
}
