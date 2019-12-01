using Chama.Dal.Containers.Client;
using Chamma.Common.Settings;
using CoursesDB.Client;
using CoursesDB.Client.Model;
using DBTester.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester
{
    public class CreateContainerCommand : ICreateContainerCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IContainerClient dbClient;

        public CreateContainerCommand(ISettingProvider settings, IWriter writer, IReader reader, IContainerClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "Cretae Conatiner"; }
        public string Key { get => "cc"; }

        public void Execute()
        {
            writer.Write("Enter Db Name > Enter > Conatiner Id > Enter > Partition Key > Enter > ThroughtPut > Enter ");
            var result = dbClient.CreateContainer(new Chama.Dal.Containers.Client.Model.CreateConatinerRequest{ 
                DbName = reader.ReadMessage(),
                ConatinerId = reader.ReadMessage(),
                PartiotionKey = reader.ReadMessage(),
                Throughput  = int.Parse(reader.ReadMessage())
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
             writer.Write($"Conatiner with ID : {result.ContainerId} Added");
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
