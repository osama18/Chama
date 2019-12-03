using Chama.Dal.Containers;
using Chamma.Common.Logging;
using Chamma.Common.Settings;
using CoursesDB;
using DBTester.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using StructureMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DBTester
{
    class Program
    {
        private static Dictionary<string, ICommad> actions = new Dictionary<string, ICommad>();
        private static IServiceProvider serviceProvider;


        static void Main(string[] args)
        {
            InitIoc();

            var commands = LoadCommads();

            ServeCustomer(commands);

            
        }

        private static void ServeCustomer(Dictionary<string, ICommad> commands)
        {
            var writer = serviceProvider.GetService<IWriter>();
            var reder = serviceProvider.GetService<IReader>();

            while (true)
            {
                writer.Write("Please enter your command number");
                foreach (string commandKey in commands.Keys)
                {
                    writer.Write($"{commandKey} - {commands[commandKey].Name}");
                }

                string input = reder.ReadMessage().ToLowerInvariant();

                if (commands.ContainsKey(input))
                {
                    commands[input].Execute();
                }
                else
                {
                    writer.Write("Unknown Command");
                }

                writer.Write("------------------------------------------");
            }
        }

        private static Dictionary<string,ICommad> LoadCommads()
        {
            var writer = serviceProvider.GetService<IWriter>();
            var command = serviceProvider.GetService<IViewContainerCommand>();
            var type = typeof(ICommad);
            var commands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(s => s.IsInterface == true && s.Name != nameof(ICommad))
                .Select(s => (ICommad)serviceProvider.GetRequiredService(s))
                .ToDictionary(s => s.Key.ToLowerInvariant(), s => s);

            writer.Write("Commands loaded");
            return commands;
        }

        private static void InitIoc()
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            //setup our DI
            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfigurationRoot>(configuration)
                .RegisterSettingsProvider()
                .RegisterLoggers()
                .RegisterGenerIcRepository();

            services.RegisterAssemblyPublicNonGenericClasses(
                     Assembly.GetAssembly(typeof(Program)))
                .AsPublicImplementedInterfaces();

            serviceProvider = services.BuildServiceProvider();

            var writer = serviceProvider.GetService<IWriter>();
            writer.Write("Container built");
        }


       
    }
}
