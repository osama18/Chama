using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chamma.Common.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chama.Web.SubscribtionV1.Controllers
{
    [ApiController]
    [Route("")]
    public class AbouttController : ControllerBase
    {
        private readonly ISettingProvider settingProvider;
        public AbouttController(ISettingProvider settingProvider)
        {
            this.settingProvider = settingProvider;
        }

        [HttpGet]
        public string Get()
        {
            var Release = settingProvider.GetSetting<string>("Release");
            return $"Chama Assigment Release {Release}";
        }
    }
}
