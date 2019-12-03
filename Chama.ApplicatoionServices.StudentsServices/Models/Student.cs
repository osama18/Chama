
using GenerIcRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Chama.ApplicatoionServices.StudentsServices.Model
{
    public class Student : EtagEntity
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "teacher")]
        public int Age { get; set; }

    }
}
