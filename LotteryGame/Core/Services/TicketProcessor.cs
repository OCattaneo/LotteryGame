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

            foreach(var player in PickPlayers(playerCount))
            {
                ticketCounts[player] = IsValid(_random.Next(1, 10), balanceArray[player]);
            }

            //return adjusted balanceArray, TicketCount per player (assigned in array)
            return ticketCounts;
        }

        private List<int> PickPlayers(int count)
        {
            var nums = new List<int>();

            while (nums.Count < count)
            {
                nums.Add(_random.Next(1, 14));
            }

            return nums;
        }

        private int IsValid(int count, decimal balance)
        {
            // 0 < i < 10
            // cost <  balance
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
