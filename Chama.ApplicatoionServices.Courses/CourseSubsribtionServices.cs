using Chama.ApplicatoionServices.Courses.Dtos;
using Chama.Common.Logging;
using Chama.Dal.CourseSubsribtionServices.Model;
using Chamma.Common.Settings;
using GenerIcRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.ApplicatoionServices.Courses
{
    public class CourseSubsribtionServices : ICourseSubsribtionServices
    {
        private readonly IGenericRepository<Course> coursesRspository;
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private static string _dbName;
        private static string _conatinerId;


        public CourseSubsribtionServices(IGenericRepository<Course> coursesRspository,
            ILogger logger,
            ISettingProvider settingProvider)
        {
            this.logger = logger;
            this.settingProvider = settingProvider;
            this.coursesRspository = coursesRspository;
            if (string.IsNullOrEmpty(_dbName))
            {
                _dbName = settingProvider.GetSetting<string>("CoursesDbName");
            }
            if (string.IsNullOrEmpty(_conatinerId))
            {
                _conatinerId = settingProvider.GetSetting<string>("CoursesConatiner");
            }
            
        }

        public async Task<CourseSubscribeResponse> SubscribeAsync(CourseSubscribeRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException("Request is null");

                var course = await coursesRspository.Retrieve(_dbName, _conatinerId, request.CourseId.ToString(), request.CourseId.ToString());

                if (course == null)
                {
                    throw new ArgumentNullException($"No Course Found with Id {request.CourseId}");
                }

                if (course.StudentsIds != null)
                {
                    if (course.StudentsIds.Contains(request.StudentId))
                    {
                        return new CourseSubscribeResponse { 
                            Subscribed = false,
                            ErrorMessage = "Already Registered"
                        };
                    }
                }
                else
                {
                    course.StudentsIds = new List<Guid>() { request.StudentId };
                }

                if (course.StudentsIds.Count() >= course.Capacity)
                {
                    return new CourseSubscribeResponse
                    {
                        Subscribed = false,
                        ErrorMessage = "Course is Full",
                        ErrorReason = ErrorReason.CourseComplete
                    };
                }

                course.ETag = Guid.NewGuid().ToString();
                course.Capacity++;

                try {
                    var reapsone = coursesRspository.UpdateDocumentOptimisticConcurrency(_dbName, _conatinerId, course, course.Id);
                    return new CourseSubscribeResponse { Subscribed = true};
                }
                catch (Exception ex)
                {
                    await logger.LogException(LogEvent.DeadLockSubscription, ex);
                    return new CourseSubscribeResponse
                    {
                        Subscribed = false,
                        ErrorMessage = "Course is so busy, Please try again later",
                        ErrorReason = ErrorReason.TechnicalError
                    };
                }
            }
            catch (Exception ex) {
                await logger.LogException(LogEvent.FailedToSubScribe, ex);
                return new CourseSubscribeResponse
                {
                    Subscribed = false,
                    ErrorMessage = ex.Message,
                    ErrorReason = ErrorReason.TechnicalError
                };
            }
        }
    }
}
