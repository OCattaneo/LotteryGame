namespace Core.Services
{
    public class BalanceHandler
    {
        public BalanceHandler()
        {
        }

        public (decimal[], decimal) SubtractTickets(int[] ticketCounts, decimal[] balanceArray, decimal houseBalance)
        {
            for (int i = 0; i < balanceArray.Length; i++)
            {
                balanceArray[i] = balanceArray[i] - ticketCounts[i];
                houseBalance += ticketCounts[i];
            }

            return (balanceArray, houseBalance);
        }

        public (decimal[], decimal) AddPrizeWinnings(List<List<KeyValuePair<int, decimal>>> prizes, decimal[] balanceArray, decimal houseBalance)
        {
            foreach(var tier in prizes)
            {
                foreach (var pair in tier)
                {
                    balanceArray[pair.Key] += pair.Value;
                    houseBalance -= pair.Value;
                }
            }
            return (balanceArray, houseBalance);
        }

        public bool ValidGame(decimal[] balanceArray)
        {
            var count = 0;
            foreach(var balance in balanceArray)
            {
                if (balance > 1)
                {
                    count++;
                }
            }

            if (count >= 10) return true;
            return false;
        }
    }
}
