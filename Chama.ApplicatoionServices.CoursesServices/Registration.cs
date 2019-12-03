
using CoursesDB;
using Microsoft.Extensions.DependencyInjection;

namespace Chama.ApplicatoionServices.CoursesServices
{
    public static class GenerIcRepository
    {
        public static IServiceCollection RegisterCoursesServices(this IServiceCollection collection)
        {
            collection.RegisterGenerIcRepository();
            collection.AddScoped<ICoursesServices, CoursesServices>();
            return collection.AddScoped<ICoursesMapper, CoursesMapper>();
        }
    }
}
