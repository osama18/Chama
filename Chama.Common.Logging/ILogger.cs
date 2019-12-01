using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chama.Common.Logging
{
    public interface ILogger
    {
        Task LogError(Enum logEvent, string Format);
        Task LogInformation(Enum logEvent, string Format);
        Task LogWarning(Enum logEvent, string Format);
        Task LogException(Enum logEvent, Exception ex);

    }
}
