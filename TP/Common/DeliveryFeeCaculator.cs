using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TP.Common.CustomConfig;

namespace TP.Common
{
    public class DeliveryFeeCaculator
    {
        static DispatcherConfigurationSection config =
            ConfigurationManager.GetSection("dispatcherConfigurationSection") as DispatcherConfigurationSection;

        public static decimal GetDeliveryFee(decimal distance)
        {
            var query = config?.DeliveryFees?.OfType<DeliveryFeeConfigurationElement>()
                .FirstOrDefault(i => i.MilesFrom < distance && i.MilesTo >= distance);

            return query?.DeliveryFee ?? 0;
        }

        public static decimal GetDeliveryFee(decimal distance, decimal orderAmount)
        {
            decimal result = 0;
            var query = config?.DeliveryFees?.OfType<DeliveryFeeConfigurationElement>()
                .FirstOrDefault(i => i.MilesFrom < distance && i.MilesTo >= distance);

            if (query != null)
            {
                result = query.DeliveryFee;
                var discountQuery = query.Discounts.OfType<DeliveryDiscountConfigurationElement>()
                    .FirstOrDefault(i => i.OrderFrom < orderAmount && i.OrderTo >= orderAmount);
                if (discountQuery != null)
                {
                    result -= discountQuery.DiscountAmount;
                }
            }

            return result < 0 ? 0: result;
        }

    }
}
