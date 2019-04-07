using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class CheaterIterablePlayer : IterablePlayer
    {
        public List<int> GlobalGuessedNumbers;
        public CheaterIterablePlayer()
        {
            GlobalGuessedNumbers = new List<int>();
            Name = "CheaterIterablePlayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            LastChoice = minValue - 1;
            do
            {
                do
                {
                    LastChoice++;
                    LocalGuessNumbes.Add(LastChoice);
                }
                while (GlobalGuessedNumbers.Contains(LastChoice));
                GlobalGuessedNumbers.Add(LastChoice);

            }
            while (LastChoice != numberToBeGuessed && !token.IsCancellationRequested);

            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = LastChoice
            };
        }
    }
}
