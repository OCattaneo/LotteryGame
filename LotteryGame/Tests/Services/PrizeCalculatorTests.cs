using Core.Services;
using FluentAssertions;

namespace Tests.Services
{
    public class PrizeCalculatorTests
    {
        private readonly PrizeCalculator _prizeCalculator;
        private readonly Random _random;

        public PrizeCalculatorTests()
        {
            _prizeCalculator = new PrizeCalculator();
            _random = new Random();
        }

        [Fact]
        public void CalculatePrize_ShouldReturnCorrectNumberOfWinners_AndPrizeAmount_PerStage()
        {
            //Arrange
            var ticketCounts = new int[15] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 1, 2, 3, 4 };
            var expectedFirstPrize = 27.5m;
            var expectedSecondPrize = 2.75m;
            var expectedThirdPrize = 0.5m;

            //Act
            var result = _prizeCalculator.CalucatePrize(ticketCounts);
            var firstPrizeResult = result[0];
            var secondPrizeResult = result[1];
            var thirdPrizeResult = result[2];
            var firstPrizeAmount = firstPrizeResult.FirstOrDefault().Value;
            var secondPrizeAmount = secondPrizeResult.FirstOrDefault().Value;
            var thirdPrizeAmount = thirdPrizeResult.FirstOrDefault().Value;

            //Assert
            firstPrizeResult.Should().HaveCount(1);
            secondPrizeResult.Should().HaveCount(6);
            thirdPrizeResult.Should().HaveCount(11);
            firstPrizeAmount.Should().Be(expectedFirstPrize);
            secondPrizeAmount.Should().Be(expectedSecondPrize);
            thirdPrizeAmount.Should().Be(expectedThirdPrize);
        }
    }
}
