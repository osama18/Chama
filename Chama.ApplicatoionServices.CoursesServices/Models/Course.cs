﻿
using GenerIcRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Chama.ApplicatoionServices.CoursesServices.Model
{
    public class Course : EtagEntity
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "teacher")]
        public Guid TeacheId { get; set; }

        [JsonProperty(PropertyName = "students")]
        public IEnumerable<Guid> StudentsIds { get; set; }

        [JsonProperty(PropertyName = "Capacity")]
        public int Capacity { get; set; }
    }
}
