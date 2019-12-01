using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesDB.Client.Model
{
    public class GetDataBasesResponse : Response
    {
        public FeedResponse<DatabaseProperties> DataBases { get; set; }
    }
}
