using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PrizeCalculator
    {
        private readonly Random _random = new();

        public PrizeCalculator() 
        {   
        }

        public List<List<KeyValuePair<int, decimal>>> CalucatePrize(int[] ticketCounts)
        {
            var totalPrize = 0;
            var winnerList = new List<int>();
            List<List<KeyValuePair<int, decimal>>> winners = new List<List<KeyValuePair<int, decimal>>>();

            foreach (var count in ticketCounts)
            {
                totalPrize += count;
            }

            var winner = GetWinner(_random.Next(totalPrize), ticketCounts);
            winnerList.Add(winner);
            var grandPrize = Math.Round((decimal)totalPrize / 2, 2);
            var winnerValues = new List<KeyValuePair<int, decimal>>();
            winnerValues.Add(new KeyValuePair<int, decimal>(GetWinner(winner, ticketCounts), grandPrize));
            winners.Add(winnerValues);

            var secondaryCount = (int)Math.Round((decimal)totalPrize * 0.1m);
            var secondaryWinners = PickWinners(secondaryCount, winnerList, totalPrize, ticketCounts);
            var secondaryPrize = Math.Round(Math.Round((decimal)totalPrize * 0.3m, 2)/secondaryWinners.Count, 2);
            var secondaryWinnerValues = new List<KeyValuePair<int, decimal>>();

            foreach (var secondaryWinner in secondaryWinners)
            {
                winnerList.Add(secondaryWinner);
                secondaryWinnerValues.Add(new KeyValuePair<int, decimal>(GetWinner(secondaryWinner, ticketCounts), secondaryPrize));
            }
            
            winners.Add(secondaryWinnerValues);

            var tertiaryCount = (int)Math.Round((decimal)totalPrize * 0.2m);
            var tertiaryWinners = PickWinners(tertiaryCount, winnerList, totalPrize, ticketCounts);
            var tertiaryPrize = Math.Round(Math.Round((decimal)totalPrize * 0.1m, 2) / tertiaryWinners.Count, 2);
            var tertiaryWinnerValues = new List<KeyValuePair<int, decimal>>();

            foreach (var tertiaryWinner in tertiaryWinners)
            {
                tertiaryWinnerValues.Add(new KeyValuePair<int, decimal>(GetWinner(tertiaryWinner, ticketCounts), tertiaryPrize));
            }

            winners.Add(tertiaryWinnerValues);

            return winners;

        }

        private List<int> PickWinners(int count, List<int> used, int total, int[] ticketCounts)
        {
            var nums = new List<int>();

            //need to sort this out
            while (nums.Count < count)
            {
                int num = 0;
                do
                {
                    num = _random.Next(1, total);
                } while (used.Contains(GetWinner(num, ticketCounts)));
                nums.Add(GetWinner(num, ticketCounts));
                used.Add(GetWinner(num, ticketCounts));
            }

            return nums;
        }

        private int GetWinner(int value, int[] ticketCounts)
        {
            for (int i = 0; i < ticketCounts.Length; i++)
            {
                value -= ticketCounts[i];
                if (value <= 0) return i;
            }
            return 0;
        }
    }
}
