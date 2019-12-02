using Chama.Dal.Containers;
using CoursesDB.Client;
using GenerIcRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesDB
{
    public static class GenerIcRepository
    {
        public static IServiceCollection RegisterGenerIcRepository(this IServiceCollection collection)
        {
            collection.RegisterContainerClient();
            return collection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
