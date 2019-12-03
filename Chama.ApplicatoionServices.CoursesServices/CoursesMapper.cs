using Chama.ApplicatoionServices.CoursesServices.Dto;
using Chama.ApplicatoionServices.CoursesServices.Model;
using Chama.Common.Logging;


namespace Chama.ApplicatoionServices.CoursesServices
{
    public class CoursesMapper : ObjectMapper, ICoursesMapper
    {
        public CoursesMapper()
        {
            base.Configure();
        }
        protected override void CreateMappings(AutoMapper.IMapperConfiguration cfg)
        {
            cfg.CreateMap<Course, CourseDto>();
            cfg.CreateMap<CourseDto, Course> ();
        }
    }
}