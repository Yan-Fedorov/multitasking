using ChallangeX1.GuessNumberModels;
using ChallangeX1.Services;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class IterablePlayer : BasicPlayer
    {
        private IGlobalDataManager _globalDataManager;

        public IterablePlayer(IGlobalDataManager globalDataManager)
        {
            _globalDataManager = globalDataManager;
            Name = "IterablePlayer";
            LocalGuessNumbes = new List<int>();
        }
        public override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            LastChoice = minValue-1;
            do
            {
                if (LastChoice >= minValue)
                {
                    _globalDataManager.SetGlobalGuesses(LastChoice);
                }
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

