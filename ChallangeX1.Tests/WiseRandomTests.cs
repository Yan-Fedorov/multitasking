using ChallangeX1.Entities;
using ChallangeX1.Services;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace ChallangeX1.Tests
{
    public class WiseRandomTests
    {
        private IGlobalDataManager dataManager;
        CancellationTokenSource cancelToken;
        CancellationToken ct;
        public WiseRandomTests()
        {
            cancelToken = new CancellationTokenSource();
            ct = cancelToken.Token;
            dataManager = new GlobalDataManager();
        }
        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            WiseRandomPlayer player = new WiseRandomPlayer(dataManager);
            player.LocalGuessNumbes.AddRange(new List<int>
            {
                3
            });

            // Act
            var result = player.MakeChoice(0, 5, 4, ct);

            // Assert
            Assert.False(dataManager.CheckGlobalGuesses(3));
        }
    }
}
