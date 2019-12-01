using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesDB.Client.Model
{
    public class CreateDbResponse : Response
    {
        public DatabaseProperties DataBase { get; set; }
    }
}
