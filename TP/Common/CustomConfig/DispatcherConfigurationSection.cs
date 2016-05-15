using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Common;
using TP.Common.CustomConfig;

namespace TP.Common.CustomConfig
{
    public class DispatcherConfigurationSection : ConfigurationSection
    {

        [ConfigurationProperty("deliveryFees", IsRequired = false)]
        public GenericConfigurationElementCollection<DeliveryFeeConfigurationElement> DeliveryFees
        {
            get { return (GenericConfigurationElementCollection<DeliveryFeeConfigurationElement>)this["deliveryFees"]; }
        }

        [ConfigurationProperty("startups", IsRequired = false)]
        public GenericConfigurationElementCollection<StartupConfigurationElement> Startups
        {
            get { return (GenericConfigurationElementCollection<StartupConfigurationElement>)this["startups"]; }
        }
    }
}
