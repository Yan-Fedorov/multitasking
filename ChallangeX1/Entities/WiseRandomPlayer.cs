using ChallangeX1.GuessNumberModels;
using ChallangeX1.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class WiseRandomPlayer : BasicPlayer
    {
        private readonly IGlobalDataManager _globalDataManager;

        //private List<int> _numbersForCheater;
        //private object _locker;
        public WiseRandomPlayer(/*List<int> numbersForCheater, object locker*/IGlobalDataManager globalDataManager)
        {
            Name = "WiseRandomPlayer";
            LocalGuessNumbes = new List<int>();
            LastChoice = 0;
            _globalDataManager = globalDataManager;
            //_numbersForCheater = numbersForCheater;
            //_locker = locker;
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            Random rnd = new Random();
            int i = 0;

            do
            {
                if (i > 0)
                {
                    _globalDataManager.SetGlobalGuesses(LastChoice);
                //    //lock (_locker)
                //    //{
                //    //    _numbersForCheater.Add(LastChoice);
                //    //}
                   i++;
                }

                do
                {
                    LastChoice = rnd.Next(minValue, maxValue + 1);
                }
                while (LocalGuessNumbes.Contains(LastChoice));
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
