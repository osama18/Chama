using Chamma.Common.Settings;
using CoursesDB.Client;
using CoursesDB.Client.Model;
using DBTester.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester
{
    public class CreateDataBasesCommand : ICreateDataBasesCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDBClient dbClient;

        public CreateDataBasesCommand(ISettingProvider settings, IWriter writer, IReader reader, IDBClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "Cretae Databases"; }
        public string Key { get => "c"; }

        public void Execute()
        {
            writer.Write("Enter Db Name : ");
            var result = dbClient.CreateDb(new CreateDbRequest { 
                Name = reader.ReadMessage()
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
             writer.Write($"ID : {result.DataBase.Id} , Last Modified {result.DataBase.LastModified}");
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
