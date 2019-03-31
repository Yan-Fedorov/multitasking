using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class RandomPLayer : BasicPlayer
    {
        public RandomPLayer()
        {
            Name = "RandomPLayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationTokenSource cancelTokSSrc, CancellationToken token)
        {
            Random rnd = new Random();
            int choice;
            do
            {
                choice = rnd.Next(minValue, maxValue + 1);
                LocalGuessNumbes.Add(choice);
            }
            while (choice != numberToBeGuessed && !token.IsCancellationRequested);
            cancelTokSSrc.Cancel();
            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = choice
            };
        }
    }
}
