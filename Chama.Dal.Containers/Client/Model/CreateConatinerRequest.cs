using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Dal.Containers.Client.Model
{
    public class CreateConatinerRequest
    {
        public string DbName { get; set; }
        public string ConatinerId { get; set; }

        public int? Throughput { get; set; }

        public string PartiotionKey { get; set; }

    }
}
