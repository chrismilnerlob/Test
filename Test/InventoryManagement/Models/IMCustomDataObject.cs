using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcelandTest.InventoryManagement.Models
{
    public class IMCustomDataObject
    {
        public int SellIn { get; set; } // number of days to sell an item

        public int Quality { get; set; } // quality of item
    }
}
