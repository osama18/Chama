using GenerIcRepository;
using System;

namespace Chama.ApplicatoionServices.StudentsServices.Dto
{
    public class StudentDto 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public string ETag { get; set; }
    }
}
