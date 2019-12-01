using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Dal.Documents.Model
{
    public class Course
    {
 

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "teacher")]
        public Guid TeacheId { get; set; }

        [JsonProperty(PropertyName = "students")]
        public IEnumerable<Guid> StudentsIds { get; set; }

        [JsonProperty(PropertyName = "Capacity")]
        public IEnumerable<Guid> Capacity { get; set; }

    }
}
