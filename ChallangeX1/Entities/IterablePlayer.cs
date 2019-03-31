using ChallangeX1.GuessNumberModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallangeX1.Entities
{
    public class IterablePlayer : BasicPlayer
    {
        public IterablePlayer()
        {
            Name = "IterablePlayer";
            LocalGuessNumbes = new List<int>();
        }
        public int LastChoice { get; set; }
        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue)
        {
            if(LastChoice != 0 && LastChoice > minValue)
            {
                LocalGuessNumbes.Add(LastChoice);
                LastChoice++;
                return new MakeChoiceResult
                {
                    PlayerName = Name,
                    Choice = LastChoice
                };
            }

            LastChoice = minValue;
            return new MakeChoiceResult
            {
                PlayerName = Name,
                Choice = LastChoice
            };
        }
    }
}
