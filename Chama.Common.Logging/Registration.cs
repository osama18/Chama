
using Chama.Common.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace Chamma.Common.Loggers
{
    public static class Registration
    {
        public static IServiceCollection RegisterLoggeingServices(this IServiceCollection collection)
        {
            return collection.AddSingleton<ILogger, Logger>();
        }
    }
}
