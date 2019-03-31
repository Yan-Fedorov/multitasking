using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class CheaterIterablePlayer: IterablePlayer
    {
        public List<int> GlobalGuessedNumbers;
        public CheaterIterablePlayer()
        {
            GlobalGuessedNumbers = new List<int>();
            Name = "CheaterIterablePlayer";
            LocalGuessNumbes = new List<int>();
        }
        
        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationTokenSource cancelTokSSrc, CancellationToken token)
        {
            do
            {
                if (LastChoice > minValue)
                {
                    do
                    {
                        LastChoice++;
                        LocalGuessNumbes.Add(LastChoice);
                    }
                    while (!GlobalGuessedNumbers.Contains(LastChoice));
                    GlobalGuessedNumbers.Add(LastChoice);
                }
                else
                {
                    LastChoice = minValue;
                }
            }
            while (LastChoice != numberToBeGuessed && !token.IsCancellationRequested);

            cancelTokSSrc.Cancel();
            return new MakeChoiceResult {
                PlayerName = Name,
                Choice = LastChoice
            };
        }
    }
}
