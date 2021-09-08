using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcelandTest.InventoryManagement
{
    public class IMSellInCalculator : IIMSellInCalculator
    {
        public int CalculateSellIn(int sellIn, string name)
        {
            // Psuedo end of each day check here..

            // Soap never has to be sold so it's sellIn date does not change
            if (name.ToLower() == "soap")
            {
                return sellIn;
            }
            // sellIn reduces by 1
            return sellIn -= 1;
        }
    }

    public interface IIMSellInCalculator
    {
        int CalculateSellIn(int sellIn, string name);
    }
}
