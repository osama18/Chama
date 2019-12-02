using Chama.Dal.Containers.Client;
using CoursesDB;
using Microsoft.Extensions.DependencyInjection;

namespace Chama.Dal.Containers
{
    public static class Registration
    {
        public static IServiceCollection RegisterContainerClient(this IServiceCollection collection)
        {
            collection.RegisterDbClient();
            return collection.AddScoped<IContainerClient, ContainerClient>();
        }
    }
}
