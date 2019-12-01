using Chamma.Common.Settings;
using CoursesDB.Client;
using DBTester.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester
{
    public class DeleteDataBasesCommand : IDeleteDataBasesCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDBClient dbClient;

        public DeleteDataBasesCommand(ISettingProvider settings, IWriter writer, IReader reader, IDBClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "Delete Databases"; }
        public string Key { get => "d"; }

        public void Execute()
        {
            writer.Write("Enter Db Name");
            var result = dbClient.DeleteDb(new CoursesDB.Client.Model.DeleteDbRequest {
                Name = reader.ReadMessage()
            }).GetAwaiter().GetResult();
            if (result.Success)
            {
                writer.Write("Deleted");
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
