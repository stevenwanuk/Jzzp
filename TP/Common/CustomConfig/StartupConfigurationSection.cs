using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Common.CustomConfig;

namespace TP.Common.CustomConfig
{
    public class StartupConfigurationSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty _propUserInfo = new ConfigurationProperty(
            null,
            typeof(StartupConfigurationCollection),
            null,
            ConfigurationPropertyOptions.IsDefaultCollection
        );

        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        static StartupConfigurationSection()
        {
            _properties.Add(_propUserInfo);
        }

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public StartupConfigurationCollection Startups
        {
            get { return (StartupConfigurationCollection)base[_propUserInfo]; }
        }
    }
}
