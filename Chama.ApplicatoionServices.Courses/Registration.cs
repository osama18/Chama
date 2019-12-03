using CoursesDB;
using Microsoft.Extensions.DependencyInjection;
namespace Chama.ApplicatoionServices.SubScribtionsServices
{
    public static class GenerIcRepository
    {
        public static IServiceCollection RegisterSubscribtionServices(this IServiceCollection collection)
        {
            collection.RegisterGenerIcRepository();
            return collection.AddScoped<ICourseSubsribtionServices, CourseSubsribtionServices>();
        }
    }
}
