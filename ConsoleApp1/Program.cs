using Autofac;
using Autofac.Core;
using IcelandTest.InventoryManagement;
using IcelandTest.InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.InventoryManagement.Utils;

namespace ConsoleApp1
{
    class Program
    {
        private IGlobalUtils _globalUtils;
        private IContainer container;

        static void Main(string[] args)
        {
            List<IModule> Modules = new List<IModule>();
            var builder = new ContainerBuilder();

            foreach (var module in Modules)
            {
                builder.RegisterModule(module);
            }
            container = builder.Build();

            // using the concrete class for tests as opposed to mocking 
            IMCalculator imCalculator = new IMCalculator(_globalUtils);


            IMSellInCalculator iMSellInCalculator = new IMSellInCalculator();

            ShoppingItem shoppingItem = new ShoppingItem()
            {
                Name = "Aged Brie",
                Quality = 1,
                SellIn = 1
            };


            int sellIn = iMSellInCalculator.CalculateSellIn(shoppingItem.SellIn, shoppingItem.Name);

            // passing updated sellIn value as used to calculate quality
            int quality = imCalculator.CalculateQuality(shoppingItem.Name, shoppingItem.Quality, sellIn);

        }
    }
}
