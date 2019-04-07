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
        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            LastChoice = minValue-1;
            do
            {
                LastChoice++;
                LocalGuessNumbes.Add(LastChoice);
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

