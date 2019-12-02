using Chama.ApplicatoionServices.Courses;
using Chama.Common.Logging;
using Chama.Dal.CourseSubsribtionServices.Model;
using Chamma.Common.Settings;
using GenerIcRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Chama.ApplicatinServices.Courses.Test
{
    public class CourseSubsribtionServicesTests
    {

        private Mock<ISettingProvider> settingProviderMock = new Mock<ISettingProvider>();
        private Mock<IGenericRepository<Course>> coursesRepositotyMock = new Mock<IGenericRepository<Course>>();
        private Mock<ILogger> loggerMock = new Mock<ILogger>();

        [Fact]
        public async Task StudentSubscribe_CourseComplete_SuncribedFalse()
        {
            string coursesDbName = "CoursesDbName";
            string coursesConatiner = "CoursesConatiner";
            
            settingProviderMock
                .Setup(s => s.GetSetting<string>(It.Is<string>(v => v == "coursesDbName")))
                .Returns(coursesDbName);

            settingProviderMock
                .Setup(s => s.GetSetting<string>(It.Is<string>(v => v == "coursesConatiner")))
                .Returns(coursesConatiner);

            loggerMock
                .Setup(s => s.LogException(It.IsAny<Enum>(), It.IsAny<Exception>()));

            coursesRepositotyMock
                .Setup(s => s.Retrieve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Course { 
                    Capacity = 1,
                    StudentsIds = new List<Guid> { Guid.NewGuid()}
                });

            var objectUnderTest = new CourseSubsribtionServices(coursesRepositotyMock.Object, loggerMock.Object, settingProviderMock.Object);

            var result = await objectUnderTest.SubscribeAsync(new ApplicatoionServices.Courses.Dtos.CourseSubscribeRequest
            {
                CourseId = Guid.NewGuid(),
                StudentId = Guid.NewGuid()
            }) ;

            Assert.False(result.Subscribed);
            coursesRepositotyMock
                .Verify(s => s.UpdateDocumentOptimisticConcurrency(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Course>(), It.IsAny<string>()),Times.Never);
        }


        [Fact]
        public async Task StudentSubscribe_CourseOpen_SuncribedSuccess()
        {
            string coursesDbName = "CoursesDbName";
            string coursesConatiner = "CoursesConatiner";

            settingProviderMock
                .Setup(s => s.GetSetting<string>(It.Is<string>(v => v == "coursesDbName")))
                .Returns(coursesDbName);

            settingProviderMock
                .Setup(s => s.GetSetting<string>(It.Is<string>(v => v == "coursesConatiner")))
                .Returns(coursesConatiner);

            loggerMock
                .Setup(s => s.LogException(It.IsAny<Enum>(), It.IsAny<Exception>()));

            coursesRepositotyMock
                .Setup(s => s.Retrieve(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Course
                {
                    Capacity = 2,
                    StudentsIds = new List<Guid> { Guid.NewGuid() }
                });

            coursesRepositotyMock
                .Setup(s => s.UpdateDocumentOptimisticConcurrency(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Course>(), It.IsAny<string>())).ReturnsAsync(new Course());

            var objectUnderTest = new CourseSubsribtionServices(coursesRepositotyMock.Object, loggerMock.Object, settingProviderMock.Object);

            var result = await objectUnderTest.SubscribeAsync(new ApplicatoionServices.Courses.Dtos.CourseSubscribeRequest
            {
                CourseId = Guid.NewGuid(),
                StudentId = Guid.NewGuid()
            });

            Assert.True(result.Subscribed);
            coursesRepositotyMock
                .Verify(s => s.UpdateDocumentOptimisticConcurrency(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Course>(), It.IsAny<string>()), Times.Once);
        }

        //TODO Add more tests for concurrency
    }
}
