
using Chama.ApplicatoionServices.CoursesServices.Model;
using Chamma.Common.Settings;
using GenerIcRepository;
using System;
using System.Threading.Tasks;
using Chama.ApplicatoionServices.CoursesServices.Dto;
using Chama.Common.Loggers;

namespace Chama.ApplicatoionServices.CoursesServices
{
    public class CoursesServices : ICoursesServices
    {
        private readonly IGenericRepository<Course> coursesRspository;
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private readonly ICoursesMapper coursesMapper;
        private static string _dbName;
        private static string _conatinerId;


        public CoursesServices(IGenericRepository<Course> coursesRspository,
            ILogger logger,
            ISettingProvider settingProvider,
            ICoursesMapper coursesMapper)
        {
            this.logger = logger;
            this.settingProvider = settingProvider;
            this.coursesRspository = coursesRspository;
            this.coursesMapper = coursesMapper;
            if (string.IsNullOrEmpty(_dbName))
            {
                _dbName = settingProvider.GetSetting<string>("CoursesDbName");
            }
            if (string.IsNullOrEmpty(_conatinerId))
            {
                _conatinerId = settingProvider.GetSetting<string>("CoursesConatiner");
            }
            
        }
        public async Task<Guid> Create(CourseDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("model is null");

                var dbModel = coursesMapper.Map<Course>(model);
                var result = await coursesRspository.CreateDocument(_dbName, _conatinerId, dbModel);
                return new Guid(result);
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToAddCourse,ex);
                throw ex;
            }
        }

        public async Task<CourseDto> Update(CourseDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("model is null");

                var dbModel = coursesMapper.Map<Course>(model);
                var result = await coursesRspository.UpdateDocument(_dbName, _conatinerId, dbModel,dbModel.Id);
                return coursesMapper.Map<CourseDto>(result);
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToUpdateCourse, ex);
                throw ex;
            }
        }

        public async Task<CourseDto> Retrieve(Guid courseId)
        {
            try
            {
                var result = await coursesRspository.Retrieve(_dbName, _conatinerId, courseId.ToString(), courseId.ToString());
                return coursesMapper.Map<CourseDto>(result);
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToRetrieveCourse, ex);
                throw ex;
            }
        }

        public async Task Delete(Guid courseId)
        {
            try
            {
                await coursesRspository.DeleteDocument(_dbName, _conatinerId, courseId.ToString(), courseId.ToString());
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToDeleteCourse, ex);
                throw ex;
            }
        }
    }
}
