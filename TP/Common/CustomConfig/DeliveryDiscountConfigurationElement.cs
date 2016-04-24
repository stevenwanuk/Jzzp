using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TP.Common.CustomConfig
{
    public class DeliveryDiscountConfigurationElement : ConfigurationElement
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

        [ConfigurationProperty("orderFrom", IsRequired = true)]
        public decimal OrderFrom
        {
            get
            {
                return (decimal)this["orderFrom"];
            }
            set
            {
                this["orderFrom"] = value;
            }
        }

        [ConfigurationProperty("orderTo", IsRequired = true)]
        public decimal OrderTo
        {
            get
            {
                return (decimal)this["orderTo"];
            }
            set
            {
                this["orderTo"] = value;
            }
        }

        [ConfigurationProperty("discountAmount", IsRequired = true)]
        public decimal DiscountAmount
        {
            get
            {
                return (decimal)this["discountAmount"];
            }
            set
            {
                this["discountAmount"] = value;
            }
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
