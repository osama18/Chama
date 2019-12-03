using GenerIcRepository;
using System;
using System.Collections.Generic;

namespace Chama.ApplicatoionServices.CoursesServices.Dto
{
    public class CourseDto 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid TeacheId { get; set; }
        public IEnumerable<Guid> StudentsIds { get; set; }
        public int Capacity { get; set; }
        public string ETag { get; set; }
    }
}
