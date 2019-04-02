using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class IterablePlayer : BasicPlayer
    {
        public IterablePlayer()
        {
            Name = "IterablePlayer";
            LocalGuessNumbes = new List<int>();
        }
        public int LastChoice { get; set; }
        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationTokenSource cancelTokSSrc, CancellationToken token)
        {
            do
            {
                if (LastChoice > minValue)
                {
                    LocalGuessNumbes.Add(LastChoice);
                    LastChoice++;
                    return new MakeChoiceResult
                    {
                        PlayerName = Name,
                        Choice = LastChoice
                    };
                }
                else
                {
                    LastChoice = minValue;
                }
            }
            while (LastChoice != numberToBeGuessed && !token.IsCancellationRequested);

            //if (!cancelTokSSrc.IsCancellationRequested)
            //{
            //    cancelTokSSrc.Cancel();
            //}
            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = LastChoice
            };

        }
    }
}

