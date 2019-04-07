using ChallangeX1.GuessNumberModels;
using ChallangeX1.Services;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class CheaterIterablePlayer: BasicPlayer
    {
        public List<int> GlobalGuessedNumbers;
        private readonly object _locker;
        private readonly IGlobalDataManager _globalDataManager;

        public CheaterIterablePlayer(object locker, IGlobalDataManager globalDataManager)
        {
            Name = "CheaterIterablePlayer";
            LocalGuessNumbes = new List<int>();
            _locker = locker;
            _globalDataManager = globalDataManager;
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
                while (_globalDataManager.CheckGlobalGuesses(LastChoice));
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
