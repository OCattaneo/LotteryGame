using Core.Services;
using FluentAssertions;

namespace Tests.Services
{
    public class BalanceHandlerTests
    {
        private readonly BalanceHandler _balanceHandler;
        private readonly Random _random;

        public BalanceHandlerTests()
        {
            _balanceHandler = new BalanceHandler();
            _random = new Random();
        }

        [Fact]
        public void SubtractTickets_ShouldReturnCorrectBalances()
        {
            //Arrange
            var balanceArray = new decimal[15];
            Array.Fill(balanceArray, 10.00m);
            var ticketCounts = new int[15] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 1, 2, 3, 4};
            var houseBalance = 0.00m;
            var expectedBalanceArray = new decimal[15] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 10, 10, 9, 8, 7, 6};
            var expectedHouseBalance = 55.00m;

            //Act
            var (resultBalanceArray, resultHouseBalance) = _balanceHandler.SubtractTickets(ticketCounts, balanceArray, houseBalance);

            //Assert
            resultBalanceArray.Should().Equal(expectedBalanceArray);
            resultHouseBalance.Should().Be(expectedHouseBalance);
        }

        [Fact]
        public void AddPrizeWinnings_ShouldReturnCorrectBalances()
        {
            //Arrange
            var balanceArray = new decimal[15];
            Array.Fill(balanceArray, 5.00m);
            var houseBalance = 60.00m;
            var expectedBalanceArray = new decimal[15] { 15, 10, 10, 10, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5 };
            var expectedHouseBalance = 31.00m;
            var prizes = GeneratePrizes();

            //Act
            var (resultBalanceArray, resultHouseBalance) = _balanceHandler.AddPrizeWinnings(prizes, balanceArray, houseBalance);

            //Assert
            resultBalanceArray.Should().Equal(expectedBalanceArray);
            resultHouseBalance.Should().Be(expectedHouseBalance);
        }

        [Fact]
        public void ValidGame_ShouldReturnFalse_IfLessThan10Balances_Above1()
        {
            //Arrange
            var balanceArray = new decimal[15] { 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 0.1m, 1, 1, 1, 1 };

            //Act
            var result = _balanceHandler.ValidGame(balanceArray);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ValidGame_ShouldReturnTrue_IfMoreThan10Balances_Above1()
        {
            //Arrange
            var balanceArray = new decimal[15] { 0.1m, 0.1m, 0.1m, 0.1m, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            //Act
            var result = _balanceHandler.ValidGame(balanceArray);

            //Assert
            result.Should().BeFalse();
        }

        private List<List<KeyValuePair<int, decimal>>> GeneratePrizes()
        {
            var totalPrizes = new List<List<KeyValuePair<int, decimal>>>();
            var firstStagePrizes = new List<KeyValuePair<int, decimal>>();
            var secondStagePrizes = new List<KeyValuePair<int, decimal>>();
            var thirdStagePrizes = new List<KeyValuePair<int, decimal>>();

            var firstStage = new KeyValuePair<int, decimal>(0, 10.00m);
            firstStagePrizes.Add(firstStage);
            totalPrizes.Add(firstStagePrizes);

            var secondStage1 = new KeyValuePair<int, decimal>(1, 5);
            var secondStage2 = new KeyValuePair<int, decimal>(2, 5);
            var secondStage3 = new KeyValuePair<int, decimal>(3, 5);
            secondStagePrizes.Add(secondStage1);
            secondStagePrizes.Add(secondStage2);
            secondStagePrizes.Add(secondStage3);
            totalPrizes.Add(secondStagePrizes);

            var thirdStage1 = new KeyValuePair<int, decimal>(4, 1);
            var thirdStage2 = new KeyValuePair<int, decimal>(5, 1);
            var thirdStage3 = new KeyValuePair<int, decimal>(6, 1);
            var thirdStage4 = new KeyValuePair<int, decimal>(7, 1);
            thirdStagePrizes.Add(thirdStage1);
            thirdStagePrizes.Add(thirdStage2);
            thirdStagePrizes.Add(thirdStage3);
            thirdStagePrizes.Add(thirdStage4);
            totalPrizes.Add(thirdStagePrizes);

            return totalPrizes;
        }
    }
}
