using System.Text.RegularExpressions;

namespace XudonaxBot.DiceParser
{
    public static class DiceParser
    {
        private const string AmountGroupName = "amount";
        private const string FacesGroupName = "faces";
        private const string OperationGroupName = "op";
        private const string ModifierGroupName = "mod";
        private const string RegexString = $@"^\s?(?<{AmountGroupName}>[0-9]+)?d(?<{FacesGroupName}>[0-9]+)(\s?(?<{OperationGroupName}>\+|-|\*)\s?(?<{ModifierGroupName}>[0-9]+?))?$";

        private static readonly Regex DiceRegex = new(RegexString, RegexOptions.Compiled, TimeSpan.FromMilliseconds(500));

        public static Die Parse(string diceString)
        {
            var match = DiceRegex.Match(diceString);

            var amountString = match.Groups.GetValueOrDefault(AmountGroupName)?.Value ?? string.Empty;
            var facesString = match.Groups.GetValueOrDefault(FacesGroupName)?.Value ?? string.Empty;
            var operationString = match.Groups.GetValueOrDefault(OperationGroupName)?.Value ?? string.Empty;
            var modifierString = match.Groups.GetValueOrDefault(ModifierGroupName)?.Value ?? string.Empty;

            if (!int.TryParse(amountString, out var amount)) amount = 1;
            if (!int.TryParse(facesString, out var faces)) throw new ArgumentException("Can't roll a die with no face", nameof(diceString));
            if (!int.TryParse(modifierString, out var modifier)) modifier = 0;

            var operation = operationString switch
            {
                "+" => '+',
                "-" => '-',
                "*" => '*',
                _ => ' ',
            };

            return new Die(amount, faces, operation, modifier);
        }
    }
}
