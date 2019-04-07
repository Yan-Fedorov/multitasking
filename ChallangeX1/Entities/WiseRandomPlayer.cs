﻿using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public class WiseRandomPlayer: BasicPlayer
    {
        public WiseRandomPlayer()
        {
            Name = "WiseRandomPlayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token)
        {
            Random rnd = new Random();
            int choice;

            do
            {
                do
                {
                    choice = rnd.Next(minValue, maxValue + 1); 
                }
                while (LocalGuessNumbes.Contains(choice));
                LocalGuessNumbes.Add(choice);

            }
            while (choice != numberToBeGuessed && !token.IsCancellationRequested);

            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = choice
            };
        }
    }
}
