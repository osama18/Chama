using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chamma.Common.Settings
{
    public static class Registration
    {
        public static IServiceCollection RegisterSettingsProvider(this IServiceCollection collection)
        {
            return collection.AddSingleton<ISettingProvider, ConfigSettingsProvider>();
        }
    }
}
