﻿using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Dal.Containers.Client.Model
{
    public class GetContainersRequest : Response
    {
        public string Name { get; set; }
    }
}
