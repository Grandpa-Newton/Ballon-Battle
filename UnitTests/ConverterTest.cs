using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GraphicsOpenGL;

namespace UnitTests
{
    [TestClass]
    public class ConverterTest
    {
        [DataTestMethod]
        [DataRow(0.2f, -0.4f, new float[] { 0.6f, 0.7f})]
        [DataRow(0.0f, 0.0f, new float[] { 0.5f, 0.5f })]
        [DataRow(-0.2f, -0.3f, new float[] { 0.4f, 0.65f })]
        [DataRow(1.0f, 0.5f, new float[] { 1.0f, 0.25f })]
        [DataRow(-0.8f, 0.4f, new float[] { 0.1f, 0.3f })]
        public void ConverterTestMethod(float pointX, float pointY, float[] expected)
        {
            // Arrange
            float[] actual;

            // Act
            actual = CoordinatesConverter.Convert(pointX, pointY);

            // Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

     /*   [TestMethod]
        public void ConverterTestMethod2()
        {
            // Arrange
            float pointX = 0.0f;
            float pointY = 0.0f;
            float[] expected = { 0.5f, 0.5f };

            // Act
            float[] actual = CoordinatesConverter.Convert(pointX, pointY);

            // Assert

            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }*/
    }
}
