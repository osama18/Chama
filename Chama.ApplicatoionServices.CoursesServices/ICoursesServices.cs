using Chama.ApplicatoionServices.CoursesServices.Dto;
using System;
using System.Threading.Tasks;

namespace Chama.ApplicatoionServices.CoursesServices
{
    public interface ICoursesServices
    {
        Task<Guid> Create(CourseDto model);
        Task<CourseDto> Update(CourseDto model);

        Task<CourseDto> Retrieve(Guid courseId);

        Task Delete(Guid courseId);
    }
}