using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;

namespace ChallangeX1.Services
{
    public interface IGuessNumberService
    {
        List<GuessNumberResponse> PlayGame(GuessNumberRequest request);
    }
}
