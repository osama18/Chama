using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Dal.Containers.Client.Model
{
    public class GetContainersResponse : Response
    {
        public FeedResponse<ContainerProperties> Containers { get; set; }
    }
}
