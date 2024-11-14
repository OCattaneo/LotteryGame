using System.Collections.Generic;

namespace Core.Services
{
    public class TicketProcessor
    {
        private readonly Random _random = new();

        public TicketProcessor() 
        { 
        }

        public int[] Process(int count, decimal[] balanceArray)
        {
            var ticketCounts = new int[15];
            Array.Fill(ticketCounts, 0);
            ticketCounts[0] = IsValid(count, balanceArray[0]);

            var playerCount = _random.Next(9, 14);

            for (int i = 1; i <= playerCount; i++ )
            {
                ticketCounts[i] = IsValid(_random.Next(1, 10), balanceArray[i]);
            }

            return ticketCounts;
        }

        private int IsValid(int count, decimal balance)
        {
            if (count > 10)
            {
                count = 10;
            }

            if (count > balance)
            {
                return (int)Math.Floor(balance);
            }

            return count;
        }
    }
}
