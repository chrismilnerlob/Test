using IcelandTest.InventoryManagement;
using IcelandTest.InventoryManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace InventoryManagementTests.UnitTests
{
    [TestClass]
    public class InventoryManagementUnitTests
    {
        private IMQualityCalculator qualityCalculator;
        private IMSellInCalculator sellInCalculator;
        private IMRenderUserInput displayOutput;

        [TestInitialize]
        public void Init()
        {
            // using the concrete class for tests as opposed to mocking 
            qualityCalculator = new IMQualityCalculator();

            sellInCalculator = new IMSellInCalculator();
            displayOutput = new IMRenderUserInput(qualityCalculator, sellInCalculator);
        }

        // Interaction Tests 
        [TestMethod]
        public void OutputUserInput_WhenCalledWithCalculatedSellInAndQualityValue_ReturnsCorrectSingleOutput()
        {
            ShoppingItem shoppingItem = new ShoppingItem()
            {
                Name = "Fresh Item",
                SellIn = -1,
                Quality = 5
            };

            int sellIn = sellInCalculator.CalculateSellIn(shoppingItem.SellIn, shoppingItem.Name);
            int quality = qualityCalculator.CalculateQuality(shoppingItem.Name, shoppingItem.Quality, shoppingItem.SellIn);
            string formattedUserInput = displayOutput.OutputUserInput(shoppingItem.Name, sellIn, quality);

            Assert.AreEqual("Fresh Item -2 1", formattedUserInput);
        }

        [TestMethod]
        public void OutputMultipleUserInput_WhenCalledWithCalculatedSellInAndQualityValues_ReturnsCorrectMultipleOutput()
        {

            List<ShoppingItem> shoppingItems = new List<ShoppingItem>()
            {
                new ShoppingItem()
                {
                    Name = "Aged Brie",
                    SellIn = 1,
                    Quality = 1
                },
                new ShoppingItem()
                {
                    Name = "Christmas Crackers",
                    SellIn = -1,
                    Quality = 2
                },
                new ShoppingItem()
                {
                    Name = "Christmas Crackers",
                    SellIn = 9,
                    Quality = 2
                },
                new ShoppingItem()
                {
                    Name = "Soap",
                    SellIn = 2,
                    Quality = 2
                },
                new ShoppingItem()
                {
                    Name = "Frozen Item",
                    SellIn = -1,
                    Quality = 55
                },
                new ShoppingItem()
                {
                    Name = "Frozen Item",
                    SellIn = 2,
                    Quality = 2
                },
                new ShoppingItem()
                {
                    Name = "INVALID ITEM",
                    SellIn = 2,
                    Quality = 2
                },
                new ShoppingItem()
                {
                    Name = "Fresh Item",
                    SellIn = 2,
                    Quality = 2
                },
                new ShoppingItem()
                {
                    Name = "Fresh Item",
                    SellIn = -1,
                    Quality = 5
                },
            };

            List<ShoppingItem> calculatedShoppingItems = displayOutput.CalculatedShoppingItemsForOutput(shoppingItems);

            List<string> expectedShoppingItemResult = new List<string>()
            {
                "Aged Brie 0 2",
                "Christmas Crackers -2 0",
                "Christmas Crackers 8 4",
                "Soap 2 2",
                "Frozen Item -2 50",
                "Frozen Item 1 1",
                "NO SUCH ITEM",
                "Fresh Item 1 0",
                "Fresh Item -2 1"
            };


            List<string> multipleOutputResultAfterCalculation = displayOutput.OutputMultipleUserInput(calculatedShoppingItems);

            CollectionAssert.AreEqual(expectedShoppingItemResult, multipleOutputResultAfterCalculation);
        }

        // Unit Tests 
        /* I'd likely split these up and add more for every given scenario,
        I'm looking for oversights in the code at this point
        (Not an exhaustive list of tests) */

        [TestMethod]
        public void QualityCalculator_WhenCalledWithVariousValues_ReturnsCorrectCalculatedQuality()
        {
            Assert.AreEqual(2, qualityCalculator.CalculateQuality("AgEd BrIe", 1, 1));
            Assert.AreEqual(0, qualityCalculator.CalculateQuality("CHRISTMAS crackers", -1, 2));
            Assert.AreEqual(5, qualityCalculator.CalculateQuality("SoaP", 5, 5));
            Assert.AreEqual(1, qualityCalculator.CalculateQuality("FROZEN ITEM", 2, 2));
        }

        [TestMethod]
        public void SellInCalculator_WhenCalledWithVariousValues_ReturnsCorrectCalculatedSellIn()
        {
            Assert.AreEqual(0, sellInCalculator.CalculateSellIn(1, "AgEd BrIe"));
            Assert.AreEqual(-2, sellInCalculator.CalculateSellIn(-1, "CHRISTMAS crackers"));
            Assert.AreEqual(5, sellInCalculator.CalculateSellIn(5, "SoaP"));
            Assert.AreEqual(1, sellInCalculator.CalculateSellIn(2, "FROZEN ITEM"));
        }

        [TestMethod]
        public void CalculateChristmasCrackersQuality_WhenCalledWithVariousValues_ReturnsCorrectCalculatedQuality()
        {
            Assert.AreEqual(4, qualityCalculator.CalculateChristmasCrackersQuality(9, 2));
            Assert.AreEqual(12, qualityCalculator.CalculateChristmasCrackersQuality(5, 9));
            Assert.AreEqual(18, qualityCalculator.CalculateChristmasCrackersQuality(-9, 20));
        }

        [TestMethod]
        public void GetMaxQuality_WhenCalledWithVariousValues_ReturnsCorrectMaxQuality()
        {
            Assert.AreEqual(2, qualityCalculator.GetMaxQuality(2));
            Assert.AreEqual(50, qualityCalculator.GetMaxQuality(50));
            Assert.AreEqual(50, qualityCalculator.GetMaxQuality(56));
        }

        [TestMethod]
        public void GetMinQuality_WhenCalledWithVariousValues_ReturnsCorrectMinQuality()
        {
            Assert.AreEqual(15, qualityCalculator.GetMinQuality(15));
            Assert.AreEqual(0, qualityCalculator.GetMinQuality(-10));
            Assert.AreEqual(0, qualityCalculator.GetMinQuality(0));
        }
    }
}
