using Chama.Dal.Containers.Client;
using Chamma.Common.Settings;
using CoursesDB.Client;
using DBTester.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester
{
    public class ViewContainerCommand : IViewContainerCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IContainerClient dbClient;

        public ViewContainerCommand(ISettingProvider settings, IWriter writer, IReader reader, IContainerClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "View Conatiner"; }
        public string Key { get => "vc"; }

        public void Execute()
        {
            writer.Write("Enter Db Name : ");
            var result = dbClient.GetConatiners(new GetContainersRequest
            {
                DbName = reader.ReadMessage()
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
                foreach (var conatiner in result.Containers)
                {
                    writer.Write($"ID : {conatiner.Id} , Last Modified {conatiner.LastModified} , PartitionKey : {conatiner.PartitionKeyPath}");
                }
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
