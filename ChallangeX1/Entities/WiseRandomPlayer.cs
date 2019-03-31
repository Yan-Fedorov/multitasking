using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;

namespace ChallangeX1.Entities
{
    public class WiseRandomPlayer: RandomPLayer
    {
        public WiseRandomPlayer()
        {
            Name = "WiseRandomPlayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue)
        {
            int choice;
            Random rnd = new Random();

            do
            {
                choice = rnd.Next(minValue, maxValue + 1);
            }
            while (!LocalGuessNumbes.Contains(choice));

            LocalGuessNumbes.Add(choice);
            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = choice
            };
        }
    }
}
