using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Common
{
    public class StartupConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("startupCommmand", IsRequired = true)]
        public string StartupCommmand
        {
            get
            {
                return (string)this["startupCommmand"];
            }
            set
            {
                this["startupCommmand"] = value;
            }
        }

        [ConfigurationProperty("fileUri", IsRequired = true)]
        public string FileUri
        {
            get
            {
                return (string)this["fileUri"];
            }
            set
            {
                this["fileUri"] = value;
            }
        }

        [ConfigurationProperty("displayName", IsRequired = true)]
        public string DisplayName
        {
            get
            {
                return (string)this["displayName"];
            }
            set
            {
                this["displayName"] = value;
            }
        }

        [ConfigurationProperty("isNavigation", IsRequired = false, DefaultValue = false)]
        public bool IsNavigation
        {
            get
            {
                return (bool)this["isNavigation"];
            }
            set
            {
                this["isNavigation"] = value;
            }
        }
        internal string Key
        {
            get { return StartupCommmand; }
        }

        
    }
}
