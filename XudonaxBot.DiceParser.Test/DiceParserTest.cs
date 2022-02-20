using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XudonaxBot.DiceParser.Test
{
    [TestClass]
    public class DiceParserTest
    {
        [DataTestMethod]
        [DataRow("d6", 1, 6, ' ', 0)]
        [DataRow("2d6", 2, 6, ' ', 0)]
        [DataRow("2d6+7", 2, 6, '+', 7)]
        [DataRow(" 2d6 + 3", 2, 6, '+', 3)]
        [DataRow("10d10+8", 10, 10, '+', 8)]
        [DataRow("d20-3", 1, 20, '-', 3)]
        [DataRow("d100*2", 1, 100, '*', 2)]
        public void ParseTest(string input, int amount, int faces, char operation, int modifier)
        {
            var die = DiceParser.Parse(input);

            die.Amount.Should().Be(amount);
            die.Faces.Should().Be(faces);
            die.ModifierOperation.Should().Be(operation);
            die.ModifierAmount.Should().Be(modifier);
        }

        [TestMethod]
        public void FacesArgumentExceptionTest()
        {
            Action testMethod = () => DiceParser.Parse(string.Empty);

            testMethod.Should().Throw<ArgumentException>();
        }
    }
}
