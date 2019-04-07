using ChallangeX1.Entities;
using ChallangeX1.GuessNumberModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            var result = new List<GuessNumberResponse>();
            var cheaterIterablePlayer = new CheaterIterablePlayer();
            var iterablePlayer = new IterablePlayer();
            var randomPlayer = new RandomPLayer();
            //randomPlayer.SendNum += TestService.SendNumToCheater;
            var wiseRandomPlayer = new WiseRandomPlayer();
            
            var players = new List<BasicPlayer>();
            //players.Add(cheaterIterablePlayer);
            players.Add(iterablePlayer);
            //players.Add(randomPlayer);
            //players.Add(wiseRandomPlayer);

            var tasks = new List<Task<MakeChoiceResult>>();
            CancellationTokenSource cancelToken = new CancellationTokenSource();
            CancellationToken ct = cancelToken.Token;
            foreach (var player in players)
            {
                tasks.Add(Task.Factory.StartNew(() => player.MakeChoice(request.MinValue, request.MaxValue, request.GuessedNumber, ct), ct));
            }

            var tasksArr = tasks.ToArray();
            Task.WaitAny(tasksArr);

            cancelToken.Cancel();

            foreach (var task in tasks)
            {
                if (task.Result.Choice == request.GuessedNumber)
                {
                    result = GenerateStatisticForGame(task.Result.PlayerName, players);
                    break;
                }
                //todo: cheater add all lists
            }
            return result;

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
                else
                {
                    result.Add(new GuessNumberResponse
                    {
                        PlayerName = player.Name,
                        PlayerGuesses = player.LocalGuessNumbes,
                        isWinner = false
                    });
                }
            }
            return result;
        }
    }
}
