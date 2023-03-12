using System;

namespace AdsInfoAccessor
{
    /// <summary>
    /// Get info from native configuration provider of dotNet
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InfoFromConfigurationAttribute : InfoBaseAttribute
    {
        public InfoFromConfigurationAttribute(
            string key)
        {
            Key = key;
        }

        public string Key { get; set; }
    }
}
