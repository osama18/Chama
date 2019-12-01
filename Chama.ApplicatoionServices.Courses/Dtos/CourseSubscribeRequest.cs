using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.ApplicatoionServices.Courses.Dtos
{
    public class CourseSubscribeRequest
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
    }
}
