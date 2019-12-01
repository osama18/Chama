using CoursesDB.Client;
using GenerIcRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.ApplicatoionServices.Courses
{
    public static class GenerIcRepository
    {
        public static IServiceCollection RegisterGenerIcRepository(this IServiceCollection collection)
        {
            return collection.AddScoped<ICourseSubsribtionServices, CourseSubsribtionServices>();
        }
    }
}
