namespace Core.Services
{
    public class BalanceHandler
    {
        public BalanceHandler()
        {
        }

        public decimal[] SubtractTickets(int[] ticketCounts, decimal[] balanceArray)
        {
            for (int i = 0; i < balanceArray.Length; i++)
            {
                balanceArray[i] = balanceArray[i] - ticketCounts[i];
            }

            return balanceArray;
        }
    }
}
