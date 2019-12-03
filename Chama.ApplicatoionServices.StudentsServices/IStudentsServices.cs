using Chama.ApplicatoionServices.StudentsServices.Dto;
using System;
using System.Threading.Tasks;

namespace Chama.ApplicatoionServices.StudentsServices
{
    public interface IStudentsServices
    {
        Task<Guid> Create(StudentDto model);
        Task<StudentDto> Update(StudentDto model);

        Task<StudentDto> Retrieve(Guid studentId);

        Task Delete(Guid studentId);
    }
}