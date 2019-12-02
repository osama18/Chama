using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.Web.SubscribtionV1.Requests
{
    public class SubscribeRequest
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        
    }
}
