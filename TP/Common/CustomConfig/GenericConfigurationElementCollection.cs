using System.Configuration;

namespace TP.Common.CustomConfig
{

    [ConfigurationCollection(typeof(ConfigurationElement))]
    public class GenericConfigurationElementCollection<T> : ConfigurationElementCollection where T : ConfigurationElement, new()
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)(element)).ToString();
        }
        public T this[int idx]
        {
            get { return (T)BaseGet(idx); }
        }
    }
}
