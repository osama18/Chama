using Chama.ApplicatoionServices.StudentsServices;
using Chama.ApplicatoionServices.StudentsServices.Dto;
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
        private readonly IStudentsServices studentsServices;
        

        public SeedDataBasesCommand(ISettingProvider settings,
            IWriter writer,
            IReader reader,
            IDBClient dbClient,
            IContainerClient containerClient,
            IStudentsServices studentsServices)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
            this.containerClient = containerClient;
            this.studentsServices = studentsServices;
        }

        public string Name { get => "Seed Databases"; }
        public string Key { get => "c"; }

        public void Execute()
        {
            string dbname = settings.GetSetting<string>("CoursesDbName");
            string studentsCont = settings.GetSetting<string>("StudentsContainer");
            string coursesCont = settings.GetSetting<string>("CoursesConatiner");
            if (CreateDataBase(dbname))
            {
                if (CreateContainer(studentsCont,dbname))
                {
                    if (CreateContainer(coursesCont, dbname))
                    {
                        SeedStudents(studentsCont);
                        SeeCourses(coursesCont);
                    }
                }
            }
        }

        private bool SeeCourses(string container)
        {
            return true;
        }

        private void SeedStudents(string container)
        {
            studentsServices.Create(new StudentDto { Name = "Osama" , Age = 32});
        }

        private bool CreateContainer(string container, string dbName, string partionKey = "/id")
        {
            var result = containerClient.CreateContainer(new Chama.Dal.Containers.Client.Model.CreateConatinerRequest
            {
                DbName = dbName,
                ConatinerId = container,
                PartiotionKey = partionKey,
                Throughput = 400
            }).GetAwaiter().GetResult();

            if (result.Success)
            {
                writer.Write($"Conatiner with ID : {result.ContainerId} Added");
                return true;
            }
            else
            {
                writer.Write("Failed");
                return false;
            }
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
