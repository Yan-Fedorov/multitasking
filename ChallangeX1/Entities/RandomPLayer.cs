using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;
using System.Threading;
using ChallangeX1.Services;

namespace ChallangeX1.Entities
{
    public class RandomPLayer : BasicPlayer
    {
        private readonly IGlobalDataManager _globalDataManager;
        public RandomPLayer(IGlobalDataManager globalDataManager)
        {
            _globalDataManager = globalDataManager;
            Name = "RandomPLayer";
            LocalGuessNumbes = new List<int>();
            LastChoice = 0;
        }

        public override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            Random rnd = new Random();
            int i = 0;
            do
            {
                if (i > 0)
                {
                    _globalDataManager.SetGlobalGuesses(LastChoice);
                    
                }
                i++;
                LastChoice = rnd.Next(minValue, maxValue + 1);

                LocalGuessNumbes.Add(LastChoice);
            } while (LastChoice != numberToBeGuessed && !token.IsCancellationRequested);

            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = LastChoice
            };
        }
    }
}