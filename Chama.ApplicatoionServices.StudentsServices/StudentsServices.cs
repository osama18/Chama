
using Chama.ApplicatoionServices.StudentsServices.Model;
using Chamma.Common.Settings;
using GenerIcRepository;
using System;
using System.Threading.Tasks;
using Chama.ApplicatoionServices.StudentsServices.Dto;
using Chama.Common.Loggers;

namespace Chama.ApplicatoionServices.StudentsServices
{
    public class StudentsServices : IStudentsServices
    {
        private readonly IGenericRepository<Student> studentsRspository;
        private readonly ISettingProvider settingProvider;
        private readonly ILogger logger;
        private readonly IStudentsMapper studentsMapper;
        private static string _dbName;
        private static string _conatinerId;


        public StudentsServices(IGenericRepository<Student> studentsRspository,
            ILogger logger,
            ISettingProvider settingProvider,
            IStudentsMapper studentsMapper)
        {
            this.logger = logger;
            this.settingProvider = settingProvider;
            this.studentsRspository = studentsRspository;
            this.studentsMapper = studentsMapper;
            if (string.IsNullOrEmpty(_dbName))
            {
                _dbName = settingProvider.GetSetting<string>("CoursesDbName");
            }
            if (string.IsNullOrEmpty(_conatinerId))
            {
                _conatinerId = settingProvider.GetSetting<string>("StudentsConatiner");
            }
            
        }
        public async Task<Guid> Create(StudentDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("model is null");

                var dbModel = studentsMapper.Map<Student>(model);
                var result = await studentsRspository.CreateDocument(_dbName, _conatinerId, dbModel);
                return new Guid(result);
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToAddStudent,ex);
                throw ex;
            }
        }

        public async Task<StudentDto> Update(StudentDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("model is null");

                var dbModel = studentsMapper.Map<Student>(model);
                var result = await studentsRspository.UpdateDocument(_dbName, _conatinerId, dbModel,dbModel.Id);
                return studentsMapper.Map<StudentDto>(result);
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToUpdateStudent, ex);
                throw ex;
            }
        }

        public async Task<StudentDto> Retrieve(Guid studentId)
        {
            try
            {
                var result = await studentsRspository.Retrieve(_dbName, _conatinerId, studentId.ToString(), studentId.ToString());
                return studentsMapper.Map<StudentDto>(result);
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToRetrieveStudent, ex);
                throw ex;
            }
        }

        public async Task Delete(Guid studentId)
        {
            try
            {
                await studentsRspository.DeleteDocument(_dbName, _conatinerId, studentId.ToString(), studentId.ToString());
            }
            catch (Exception ex)
            {
                await logger.LogException(LogEvent.FailedToDeleteStudent, ex);
                throw ex;
            }
        }
    }
}
