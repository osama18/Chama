using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chama.Common.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer BuildContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            var appContainer = builder.Build();
            var serviceProvider = new AutofacServiceProvider(appContainer);
            return null;

        }
    }
}
