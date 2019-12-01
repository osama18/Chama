using Chama.Common.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Chamma.Common.Logging
{
    public static class Registration
    {
        public static IServiceCollection RegisterLoggers(this IServiceCollection collection)
        {
            return collection.AddSingleton<ILogger, Logger>();
        }
    }
}
