using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Dal.Containers.Client.Model
{
    public class DeleteContainerRequest
    {
        public string DbName { get; set; }
        public string ConatinerId { get; set; }
    }
}
