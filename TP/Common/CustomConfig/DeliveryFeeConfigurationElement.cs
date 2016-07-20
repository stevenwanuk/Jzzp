using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Common.CustomConfig
{
    public class DeliveryFeeConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("milesFrom", IsRequired = true)]
        public decimal MilesFrom
        {
            get
            {
                return (decimal)this["milesFrom"];
            }
            set
            {
                this["milesFrom"] = value;
            }
        }

        [ConfigurationProperty("milesTo", IsRequired = true)]
        public decimal MilesTo
        {
            get
            {
                return (decimal)this["milesTo"];
            }
            set
            {
                this["milesTo"] = value;
            }
        }

        [ConfigurationProperty("deliveryFee", IsRequired = true)]
        public decimal DeliveryFee
        {
            get
            {
                return (decimal)this["deliveryFee"];
            }
            set
            {
                this["deliveryFee"] = value;
            }
        }

        [ConfigurationProperty("discounts", IsRequired = false)]
        public GenericConfigurationElementCollection<DeliveryDiscountConfigurationElement> Discounts
        {
            get { return (GenericConfigurationElementCollection<DeliveryDiscountConfigurationElement>)this["discounts"]; }
        }

        public override string ToString()
        {
            return this.Name; 
        }
    }
}
