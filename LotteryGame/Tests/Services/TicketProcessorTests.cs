using Core.Services;
using FluentAssertions;

namespace Tests.Services
{
    public class TicketProcessorTests
    {
        private readonly TicketProcessor _ticketProcessor;

        public TicketProcessorTests()
        {
            _ticketProcessor = new TicketProcessor();
        }

        [Fact]
        public void Process_ShouldReturn_ValidTicketCounts()
        {
            //Arrange
            var balanceArray = new decimal[15];
            Array.Fill(balanceArray, 10.00m);
            var userCount = 10;

            //Act
            var result = _ticketProcessor.Process(userCount, balanceArray);

            //Assert
            var noTicketCount = result.Where(x => x.Equals(0)).Count();
            result.Should().HaveCount(15);
            noTicketCount.Should().BeLessThan(6);
            result.Should().OnlyContain(x => x <= 10 && x >= 0);
        }

    }
}
