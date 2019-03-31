using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;

namespace ChallangeX1.Entities
{
    public class RandomPLayer : BasicPlayer
    {
        public RandomPLayer()
        {
            Name = "RandomPLayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue)
        {
            Random rnd = new Random();
            int choice = rnd.Next(minValue, maxValue + 1);
            LocalGuessNumbes.Add(choice);
            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = choice
            };
        }
    }
}
