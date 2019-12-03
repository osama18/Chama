
using Chama.ApplicatoionServices.SubScribtionsServices.Dtos;
using System.Threading.Tasks;

namespace Chama.ApplicatoionServices.SubScribtionsServices
{
    public interface ICourseSubsribtionServices
    {
        Task<CourseSubscribeResponse> SubscribeAsync(CourseSubscribeRequest request);
    }
}