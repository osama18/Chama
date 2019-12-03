using Chama.ApplicatoionServices.StudentsServices.Dto;
using Chama.ApplicatoionServices.StudentsServices.Model;
using Chama.Common.Logging;
using System;
using System.Threading.Tasks;

namespace Chama.ApplicatoionServices.StudentsServices
{
    public class StudentsMapper : ObjectMapper, IStudentsMapper
    {
        protected override void CreateMappings(AutoMapper.IMapperConfiguration cfg)
        {
            cfg.CreateMap<Student, StudentDto>();
            cfg.CreateMap<StudentDto, Student> ();
        }
    }
}