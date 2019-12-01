using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chama.Common.Logging
{
    internal class Logger : ILogger
    {
        public Task LogError(Enum logEvent, string Format)
        {
            return null;
        }

        public Task LogException(Enum logEvent, Exception ex)
        {
            return null;
        }

        public Task LogInformation(Enum logEvent, string Format)
        {
            return null;
        }

        public Task LogWarning(Enum logEvent, string Format)
        {
            return null;
        }
    }
}
