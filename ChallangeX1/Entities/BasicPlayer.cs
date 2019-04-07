using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public abstract class BasicPlayer
    {
        internal string Name;
        public List<int> LocalGuessNumbes;
        public int LastChoice;
        public abstract MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationToken token);
    }
}
