using System.Security.Cryptography;

namespace XudonaxBot.DiceParser
{
    public class Die
    {
        internal Die(int amount, int faces, char modifierOperation, int modifierAmount)
        {
            Amount = amount;
            Faces = faces;
            ModifierOperation = modifierOperation;
            ModifierAmount = modifierAmount;
        }

        public int Amount { get; private set; }
        public int Faces { get; private set; }
        public char ModifierOperation { get; private set; }
        public int ModifierAmount { get; private set; }

        public (int total, int[] rolls) Roll()
        {
            var total = 0;
            var rolls = new int[Amount];

            for (var i = 0; i < Amount; i++)
            {
                var roll = RandomNumberGenerator.GetInt32(0, Faces) + 1; ;
                total += roll;
                rolls[i] = roll;
            }

            if (ModifierOperation == '+') total += ModifierAmount;
            if (ModifierOperation == '-') total -= ModifierAmount;
            if (ModifierOperation == '*') total *= ModifierAmount;

            return (total, rolls);
        }
    }
}
