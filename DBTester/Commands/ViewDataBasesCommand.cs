using Chamma.Common.Settings;
using CoursesDB.Client;
using DBTester.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester
{
    public class ViewDataBasesCommand : IViewDataBasesCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDBClient dbClient;

        public ViewDataBasesCommand(ISettingProvider settings, IWriter writer, IReader reader, IDBClient dbClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
        }
        public string Name { get => "View Databases"; }
        public string Key { get => "v"; }

        public void Execute()
        {
            var result = dbClient.GetDataBases().GetAwaiter().GetResult();
            if (result.Success)
            {
                foreach (var db in result.DataBases)
                {
                    writer.Write($"ID : {db.Id} , Last Modified {db.LastModified}");
                }
            }
            else
            {
                writer.Write("Failed");
            }
        }
    }
}
