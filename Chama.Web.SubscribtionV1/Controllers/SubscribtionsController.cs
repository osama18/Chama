using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Chama.ApplicatoionServices.Courses;
using Chama.ApplicatoionServices.Courses.Dtos;
using Chama.Common.Logging;
using Chama.Web.SubscribtionV1.Requests;
using Chamma.Common.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Chama.Web.SubscribtionV1.Controllers
{
    [ApiController]
    [Route("subscribe")]
    public class SubscribtionsController : ControllerBase
    {
        private readonly ICourseSubsribtionServices courseSubsribtionServices;
        private readonly ILogger logger;
        public SubscribtionsController(ILogger logger, ICourseSubsribtionServices courseSubsribtionServices)
        {
            this.logger = logger;
            this.courseSubsribtionServices = courseSubsribtionServices;
        }

        [HttpPut]
        public async Task<IActionResult> SubscribeAsync(SubscribeRequest request)
        {
            if (request == null)
                return BadRequest("request was null");

            try
            {
                //Todo Read the real user Id
                Guid userId = Guid.NewGuid();
                var result = await courseSubsribtionServices.SubscribeAsync(new CourseSubscribeRequest { 
                    CourseId = request.CourseId,
                    StudentId = userId
                });

                if (result.Subscribed) 
                    Ok();
                else
                    return StatusCode(500);

            }
            catch (Exception ex)
            {
                await logger.LogException(LogEventApplication.FailedToSubScribe, ex);
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
