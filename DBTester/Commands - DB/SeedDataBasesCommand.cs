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
    public class SeedDataBasesCommand : ISeedDataBasesCommand
    {
        private readonly ISettingProvider settings;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDBClient dbClient;
        private readonly IContainerClient containerClient;

        public SeedDataBasesCommand(ISettingProvider settings,
            IWriter writer,
            IReader reader,
            IDBClient dbClient,
            IContainerClient containerClient)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
            this.containerClient = containerClient;
        }
        public string Name { get => "Seed Databases"; }
        public string Key { get => "c"; }

        public void Execute()
        {
            if (CreateDataBase(settings.GetSetting<string>("CoursesDbName")))
            {
                if (CreateContainer(settings.GetSetting<string>("StudentsContainer")))
                {
                    if (CreateContainer(settings.GetSetting<string>("CoursesConatiner")))
                    {
                        if (SeedStudents(settings.GetSetting<string>("StudentsContainer")))
                        {
                            if (SeeCourses(settings.GetSetting<string>("CoursesConatiner")))
                            {

                            }
                        }
                    }
                }
            }
        }

        private bool SeeCourses(string container)
        {
            throw new NotImplementedException();
        }

        private bool SeedStudents(string container)
        {
            throw new NotImplementedException();
        }

        private bool CreateContainer(string container)
        {
            throw new NotImplementedException();
        }

        private bool CreateDataBase(string dbName)
        {
            var result = dbClient.CreateDb(new CreateDbRequest
            {
                Name = dbName
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
                writer.Write($"Data Base Created. ID : {result.DataBase.Id} , Last Modified {result.DataBase.LastModified}");
                return true;
            }
            else
            {
                writer.Write("Failed to Create Db");
                return false;
            }
        }
    }
}
