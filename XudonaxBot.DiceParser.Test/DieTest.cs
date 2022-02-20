using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XudonaxBot.DiceParser.Test
{
    [TestClass]
    public class DieTest
    {
        [DataTestMethod]
        [DataRow(1, 6, ' ', 0, 1, 6)]
        [DataRow(1, 10, ' ', 0, 1, 10)]
        [DataRow(10, 10, ' ', 1024, 1, 10 * 10)]
        [DataRow(5, 6, '+', 6, 7, 5 * 6 + 7)]
        [DataRow(5, 10, '-', 2, 3, 5 * 10 - 2)]
        public void RollTest(int amount, int faces, char operation, int modifier, int totalMin, int totalMax)
        {
            var die = new Die(amount, faces, operation, modifier);
            var (result, _) = die.Roll();

            result.Should().BeInRange(totalMin, totalMax);
        }
    }
}
