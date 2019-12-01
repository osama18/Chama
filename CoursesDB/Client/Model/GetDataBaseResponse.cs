using CoursesDB.Client.Model;
using Microsoft.Azure.Cosmos;

namespace CoursesDB.Client
{
    public class GetDataBaseResponse : Response
    {
        public Database Database { get; set; }
    }
}