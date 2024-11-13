using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PrizeCalculator
    {
        public PrizeCalculator() 
        {   
        }

        public void CalucatePrize(int[] ticketCounts)
        {
            //total of ticketCounts values =  total $
            //one ticket = 50% total - Grand Prize
            //10% of total tickets (rounded) share 10% - secondary
            //20% of total tickets (rounded) share 10% - tertiary

        }
    }
}
