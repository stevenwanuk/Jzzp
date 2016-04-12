using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Common
{

    [ConfigurationCollection(typeof(StartupConfigurationElement), AddItemName = "startupElement", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class StartupConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new StartupConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StartupConfigurationElement)element).Key;
        }

        public void Add(StartupConfigurationElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(StartupConfigurationElement element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(StartupConfigurationElement element)
        {
            if (BaseIndexOf(element) >= 0)
            {
                BaseRemove(element.Key);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public StartupConfigurationElement this[int index]
        {
            get { return (StartupConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }
}
