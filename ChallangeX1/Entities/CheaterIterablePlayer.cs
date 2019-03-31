using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;

namespace ChallangeX1.Entities
{
    public class CheaterIterablePlayer: IterablePlayer
    {
        public List<int> GlobalGuessedNumbers;
        public CheaterIterablePlayer()
        {
            GlobalGuessedNumbers = new List<int>();
            Name = "CheaterIterablePlayer";
            LocalGuessNumbes = new List<int>();
        }
        
        internal override MakeChoiceResult MakeChoice(int minValue, int maxValue)
        {
            if (LastChoice > minValue)
            {
                do
                {
                    LastChoice++;
                    LocalGuessNumbes.Add(LastChoice);
                }
                while (!GlobalGuessedNumbers.Contains(LastChoice));
                GlobalGuessedNumbers.Add(LastChoice);
                return new MakeChoiceResult
                {
                    PlayerName = Name,
                    Choice = LastChoice
                };
            }

            LastChoice = minValue;

            return new MakeChoiceResult {
                PlayerName = Name,
                Choice = LastChoice
            };
        }
    }
}
