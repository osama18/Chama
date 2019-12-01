using CoursesDB.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesDB
{
    public static class Registration
    {
        public static IServiceCollection RegisterDbClient(this IServiceCollection collection)
        {
            return collection.AddSingleton<IDBClient, DbClient>();
        }
    }
}
