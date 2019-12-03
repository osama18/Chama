using CoursesDB;
using Microsoft.Extensions.DependencyInjection;
namespace Chama.ApplicatoionServices.StudentsServices
{
    public static class GenerIcRepository
    {
        public static IServiceCollection RegisterStudentsServices(this IServiceCollection collection)
        {
            collection.RegisterGenerIcRepository();
            collection.AddScoped<IStudentsServices, StudentsServices>();
            return collection.AddScoped<IStudentsMapper, StudentsMapper>();
        }
    }
}
