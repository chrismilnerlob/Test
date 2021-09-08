using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcelandTest.InventoryManagement
{
    public class IMQualityCalculator : IIMQualityCalculator
    {
        public int CalculateQuality(string name, int quality, int sellIn)
        {
            // if incoming quality is calculated to be more than or equal to 50, return 50
            if (GetMaxQuality(quality) == 50)
            {
                return 50;
            }
            // if incoming quality is calculated to be less than or equal to 0, return 0
            if (GetMinQuality(quality) == 0)
            {
                return 0;
            }

            switch (name.ToLower())
            {
                // Take into account Min/Max quality for each new value - 
                // new calculation cannot exceed those given values

                case "aged brie":
                    quality += 1;
                    return GetMaxQuality(quality) >= 50 ? 50 : quality;

                case "christmas crackers":
                    quality = CalculateChristmasCrackersQuality(sellIn, quality);
                    return GetMaxQuality(quality) >= 50 ? 50 : quality;

                case "frozen item":
                    quality = (sellIn <= 0 ? quality -= 2 : quality -= 1);
                    return GetMinQuality(quality) < 0 ? 0 : quality;

                case "fresh item":
                    quality = (sellIn <= 0 ? quality -= 4 : quality -= 2);
                    return GetMinQuality(quality) < 0 ? 0 : quality;

                case "soap":
                    return quality;

                default:
                    break;
            }

            return quality;
        }

        public int CalculateChristmasCrackersQuality(int sellIn, int quality)
        {
            DateTime dateTime = new DateTime();

            // Increases by 2 when there are 10 days or less
            if (sellIn <= 10 && sellIn > 5)
            {
                quality += 2;
                return quality;
            }
            // Increases by 3 when there are 5 days or less
            if (sellIn <= 5 && sellIn > 0)
            {
                quality += 3;
                return quality;
            }
            if (sellIn <= 0)
            {
                quality -= 2;
            };
            // accounting for Christmas - drops to 0 after Christmas Day
            if (dateTime.Month == 12 && dateTime.Day >= 25)
            {
                quality = 0;
                return quality;
            }

            return quality;
        }

        public int GetMinQuality(int quality)
        {
            if (quality <= 0)
            {
                // min quality
                return 0;
            }
            return quality;
        }

        public int GetMaxQuality(int quality)
        {
            if (quality >= 50)
            {
                // max quality
                return 50;
            }
            return quality;
        }
    }

    public interface IIMQualityCalculator
    {
        int CalculateQuality(string name, int quality, int sellIn);
    }
}
