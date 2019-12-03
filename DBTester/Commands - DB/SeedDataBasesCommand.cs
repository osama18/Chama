using Chama.ApplicatoionServices.CoursesServices;
using Chama.ApplicatoionServices.CoursesServices.Dto;
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
        private readonly ICoursesServices coursesServices;
        
        public SeedDataBasesCommand(ISettingProvider settings,
            IWriter writer,
            IReader reader,
            IDBClient dbClient,
            IContainerClient containerClient,
            IStudentsServices studentsServices,
            ICoursesServices coursesServices)
        {
            this.settings = settings;
            this.writer = writer;
            this.reader = reader;
            this.dbClient = dbClient;
            this.containerClient = containerClient;
            this.studentsServices = studentsServices;
            this.coursesServices = coursesServices;
        }
        public string Name { get => "Seed Databases"; }
        public string Key { get => "seed"; }

        public void Execute()
        {
            var dbName = settings.GetSetting<string>("CoursesDbName");
            var studentContainer = settings.GetSetting<string>("StudentsConatiner");
            var courseContainer = settings.GetSetting<string>("CoursesConatiner");

            if (CreateDataBase(dbName))
            {
                if (CreateContainer(studentContainer, dbName))
                {
                    if (CreateContainer(courseContainer,dbName))
                    {
                        if (SeedData())
                        {
                            writer.Write("Seed completed");
                        }
                    }
                }
            }
        }

        private bool SeedData()
        {
            var studentId = studentsServices.Create(new StudentDto { 
                Name = "Osama",
                Age = 32
            }).GetAwaiter().GetResult();


            var courseId = coursesServices.Create(new CourseDto { 
                Name = "Time managment",
                Capacity = 10,
                TeacheId = Guid.NewGuid(),
                StudentsIds =new List<Guid> { studentId},
            }).GetAwaiter().GetResult();

            writer.Write($"{studentId.ToString()} subscribe to course {courseId}");
            return true;
        }

        private bool CreateContainer(string container, string dbName, string partiotionKey = "/id")
        {
            var result = containerClient.CreateContainer(new Chama.Dal.Containers.Client.Model.CreateConatinerRequest
            {
                DbName = dbName,
                ConatinerId = container,
                PartiotionKey = partiotionKey,
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
