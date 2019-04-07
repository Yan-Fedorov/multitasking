using ChallangeX1.Entities;
using ChallangeX1.Services;
using Moq;
using System.Threading;
using Xunit;

namespace ChallangeX1.Tests
{
    public class CheaterIterableTests
    {
        private Mock<IGlobalDataManager> mockDataManager;
        CancellationTokenSource cancelToken;
        CancellationToken ct;
        public CheaterIterableTests()
        {
            cancelToken = new CancellationTokenSource();
            ct = cancelToken.Token;
            mockDataManager = new Mock<IGlobalDataManager>();
        }
        [Fact]
        public void IndexViewDataMessage()
        {
            mockDataManager.Setup(x => x.CheckGlobalGuesses(It.IsAny<int>())).Returns(false);
            mockDataManager.Setup(x => x.CheckGlobalGuesses(3)).Returns(true);
            // Arrange
            CheaterIterablePlayer player = new CheaterIterablePlayer(mockDataManager.Object);

            // Act
            var result = player.MakeChoice(0, 5, 4, ct);

            // Assert
            Assert.True(!player.LocalGuessNumbes.Contains(3));
        }
    }
}
