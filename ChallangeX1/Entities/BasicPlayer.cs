using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Threading;

namespace ChallangeX1.Entities
{
    public abstract class BasicPlayer
    {
        internal string Name;

        internal List<int> LocalGuessNumbes;
        internal abstract MakeChoiceResult MakeChoice(int minValue, int maxValue, int numberToBeGuessed, CancellationTokenSource cancelTokSSrc, CancellationToken token);
    }
}
