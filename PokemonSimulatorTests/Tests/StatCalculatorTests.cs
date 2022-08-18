using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonSimulator.Calculators;
using System;

namespace PokemonSimulatorTests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void StatCalculatorCalculateHP()
    {
      // Arrange
      // Act
      var hp = StatCalculator.CalculateHP(108, 78, 24, 74);
      var attack = StatCalculator.CalculateStat(130, 78, 12, 190, 1.1);
      var defense = StatCalculator.CalculateStat(95, 78, 30, 91, 1);
      var spAttack = StatCalculator.CalculateStat(80, 78, 16, 48, 0.9);
      var spDefense = StatCalculator.CalculateStat(85, 78, 23, 84, 1);
      var speed = StatCalculator.CalculateStat(102, 78, 5, 23, 1);
      // Assert
      Assert.AreEqual(289, hp);
      Assert.AreEqual(278, attack);
      Assert.AreEqual(193, defense);
      Assert.AreEqual(135, spAttack);
      Assert.AreEqual(171, spDefense);
      Assert.AreEqual(171, speed);
    }
  }
}
