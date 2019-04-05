﻿using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;
using System.Threading;
using ChallangeX1.Services;

namespace ChallangeX1.Entities
{
    public class RandomPLayer : BasicPlayer
    {
        // Объявляем делегат
        public delegate void ChoiceHandler(int num);
        // Событие, возникающее при выводе денег
        public event ChoiceHandler SendNum;

        public RandomPLayer()
        {
            Name = "RandomPLayer";
            LocalGuessNumbes = new List<int>();
        }

        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed,
            CancellationTokenSource cancelTokSSrc, CancellationToken token)
        {
            Random rnd = new Random();
            int choice = 0;
            do
            {
                SendNum?.Invoke(choice);
                choice = rnd.Next(minValue, maxValue + 1);

                LocalGuessNumbes.Add(choice);
            } while (choice != numberToBeGuessed && !token.IsCancellationRequested);

            //if (!cancelTokSSrc.IsCancellationRequested)
            //{
            //    cancelTokSSrc.Cancel();
            //}
            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = choice
            };
        }
    }
}