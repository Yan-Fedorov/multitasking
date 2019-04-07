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
            object locker = new object();
            IGlobalDataManager globalDataManager = new GlobalDataManager();
            var iterablePlayer = new IterablePlayer(/*cheaterIterablePlayer.GlobalGuessedNumbers, locker, */globalDataManager);
            
            
            var randomPlayer = new RandomPLayer(/*cheaterIterablePlayer.GlobalGuessedNumbers, locker, */globalDataManager);
            var wiseRandomPlayer = new WiseRandomPlayer(/*cheaterIterablePlayer.GlobalGuessedNumbers, locker, */globalDataManager);
            var cheaterIterablePlayer = new CheaterIterablePlayer(locker, globalDataManager);

            var players = new List<BasicPlayer>();
            
            players.Add(iterablePlayer);
            players.Add(randomPlayer);
            players.Add(wiseRandomPlayer);
            players.Add(cheaterIterablePlayer);

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
            }
            globalDataManager.CheckGlobalGuesses(1);
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
