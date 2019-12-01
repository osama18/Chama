using Microsoft.Extensions.Configuration;
using System;

namespace Chamma.Common.Settings
{
    internal class ConfigSettingsProvider : ISettingProvider
    {
        private readonly IConfigurationRoot configurationRoot;
        public ConfigSettingsProvider(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }
        public T GetSetting<T>(string Key)
        {
            var value = configurationRoot[Key];

            if (value == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
