using ChallangeX1.Entities;
using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallangeX1.Services
{
    public class GuessNumberService : IGuessNumberService
    {
        public GuessNumberService()
        {

        }

        public List<GuessNumberResponse> PlayGame(GuessNumberRequest request)
        {
            var cheaterIterablePlayer = new CheaterIterablePlayer();
            var iterablePlayer = new IterablePlayer();
            var randomPlayer = new RandomPLayer();
            var wiseRandomPlayer = new WiseRandomPlayer();

            var players = new List<BasicPlayer>();
            players.Add(cheaterIterablePlayer);
            players.Add(iterablePlayer);
            players.Add(randomPlayer);
            players.Add(wiseRandomPlayer);

            var tasks = new List<Task<MakeChoiceResult>>();
            foreach (var player in players)
            {
                tasks.Add(new Task<MakeChoiceResult>(() => player.MakeChoice(request.MinValue, request.MinValue)));
            }

            do
            {

                foreach (var task in tasks)
                {
                    task.Start();
                }

                foreach (var task in tasks)
                {
                    task.Wait();
                }

                foreach (var task in tasks)
                {
                    if (task.Result.Choice == request.GuessedNumber)
                    {
                        return GenerateStatisticForGame(task.Result.PlayerName, players);
                    }
                    cheaterIterablePlayer.GlobalGuessedNumbers.Add(task.Result.Choice);
                }
            }
            while (true);
        }

        private List<GuessNumberResponse> GenerateStatisticForGame(string winnerName, List<BasicPlayer> players)
        {
            var result = new List<GuessNumberResponse>();
            foreach (var player in players)
            {
                if (player.Name == winnerName)
                {
                    result.Add(new GuessNumberResponse
                    {
                        PlayerName = player.Name,
                        PlayerGuesses = player.LocalGuessNumbes,
                        isWinner = true
                    });
                }
                result.Add(new GuessNumberResponse
                {
                    PlayerName = player.Name,
                    PlayerGuesses = player.LocalGuessNumbes,
                    isWinner = false
                });
            }
            return result;
        }
    }
}
