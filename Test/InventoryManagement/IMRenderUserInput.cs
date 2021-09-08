using IcelandTest.InventoryManagement.Models;
using System.Collections.Generic;

namespace IcelandTest.InventoryManagement
{
    // Examples of Dependency Injection and Dependency Inversion Principle
    public class IMRenderUserInput
    {
        private IIMQualityCalculator qualityCalculator;
        private IIMSellInCalculator sellInCalculator;
        public IMRenderUserInput(IIMQualityCalculator qualityCalculator, IIMSellInCalculator sellInCalculator)
        {
            this.qualityCalculator = qualityCalculator;
            this.sellInCalculator = sellInCalculator;
        }

        public string OutputUserInput(string name, int sellIn, int quality)
        {
            if (name.ToLower() == "invalid item")
            {
                return "NO SUCH ITEM";
            }
            return string.Join(" ", name, sellIn, quality);
        }

        public List<string> OutputMultipleUserInput(List<ShoppingItem> shoppingItems)
        {
            List<string> shoppingItemsAsString = new List<string>();

            foreach (var shoppingItem in shoppingItems)
            {
                if (shoppingItem.Name.ToLower() == "invalid item")
                {
                    shoppingItemsAsString.Add("NO SUCH ITEM");
                }
                else
                {
                    string res = string.Join(" ", shoppingItem.Name, shoppingItem.SellIn, shoppingItem.Quality);
                    shoppingItemsAsString.Add(res);
                }
            }

            return shoppingItemsAsString;
        }
        public List<ShoppingItem> CalculatedShoppingItemsForOutput(List<ShoppingItem> shoppingItems)
        {
            List<ShoppingItem> calculatedShoppingItems = new List<ShoppingItem>();
            ShoppingItem shoppingItem;

            foreach (var item in shoppingItems)
            {
                int sellIn = sellInCalculator.CalculateSellIn(item.SellIn, item.Name);

                // passing updated sellIn value as used to calculate quality
                int quality = qualityCalculator.CalculateQuality(item.Name, item.Quality, sellIn);

                shoppingItem = new ShoppingItem()
                {
                    Name = item.Name,
                    SellIn = sellIn,
                    Quality = quality
                };

                calculatedShoppingItems.Add(shoppingItem);
            }

            return calculatedShoppingItems;
        }

    }
}
