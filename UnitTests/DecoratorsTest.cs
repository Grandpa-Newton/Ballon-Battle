using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics;
using OpenTK;
using System;
using AmmoLibrary;
using AmmoLibrary.characteristics_changing;

namespace UnitTests
{
    [TestClass]
    public class DecoratorsTest
    {
        [TestMethod]
        public void DistanceDecoratorTestMethod()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            Ammo ammo;
            game.LoadGLControl();
            ammo = new ExplosiveAmmo(); // отрисовка и позиция для проверки нам не требуется
            float actualDistance;
            float expectedDistance=1.5f;

            // Act
            ammo = new DistanceDecorator(ammo);
            actualDistance = ammo.Distance;

            // Assert
            Assert.AreEqual(actualDistance, expectedDistance);
        }

        [TestMethod]
        public void RadiusDecoratorTestMethod()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            Ammo ammo;
            game.LoadGLControl();
            ammo = new SupersonicAmmo(); // отрисовка и позиция для проверки нам не требуется
            float actualRadius;
            float expectedRadius = 0.06f;

            // Act
            ammo = new RadiusDecorator(ammo);
            actualRadius = (float)Math.Round(ammo.Radius, 2); // округление из-за неточности float

            // Assert
            Assert.AreEqual(actualRadius, expectedRadius);
        }

        [TestMethod]
        public void SpeedDecoratorTestMethod()
        {
            GameWindow window = new GameWindow(1, 1, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default);
            window.Visible = false;
            BattleGame game = new BattleGame();
            Ammo ammo;
            game.LoadGLControl();
            ammo = new PiercingAmmo(); // отрисовка и позиция для проверки нам не требуется
            float actualSpeed;
            float expectedSpeed = 0.0156f;

            // Act
            ammo = new SpeedDecorator(ammo);
            actualSpeed = ammo.Speed.X;

            // Assert
            Assert.AreEqual(actualSpeed, expectedSpeed);
        }
    }
}
