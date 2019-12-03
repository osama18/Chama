using Chama.ApplicatoionServices.Courses.Dtos;
using System.Threading.Tasks;

namespace Chama.ApplicatoionServices.SubScribtionsServices
{
    public interface ICourseSubsribtionServices
    {
        Task<CourseSubscribeResponse> SubscribeAsync(CourseSubscribeRequest request);
    }
}