using System.Collections.Generic;

namespace ChallangeX1.GuessNumberModels
{
    public class GuessNumberResponse
    {
        public string PlayerName { get; set; }
        public List<int> PlayerGuesses { get; set; }
        public bool isWinner { get; set; }
    }
}
