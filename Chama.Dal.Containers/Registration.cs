using Chama.Dal.Containers.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Chama.Dal.Containers
{
    public static class Registration
    {
        public static IServiceCollection RegisterContainerClient(this IServiceCollection collection)
        {
            return collection.AddScoped<IContainerClient, ContainerClient>();
        }
    }
}
