using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;
using System.Threading;
using ChallangeX1.Services;

namespace ChallangeX1.Entities
{
    public class RandomPLayer : BasicPlayer
    {
        //public List<int> LocalGuessNumbes;
        public RandomPLayer()
        {
            Name = "RandomPLayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            Random rnd = new Random();
            int choice = 0;
            do
            {
                //SendNum?.Invoke(choice);
                choice = rnd.Next(minValue, maxValue + 1);

                LocalGuessNumbes.Add(choice);
            } while (choice != numberToBeGuessed && !token.IsCancellationRequested);

            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = choice
            };
        }
    }
}