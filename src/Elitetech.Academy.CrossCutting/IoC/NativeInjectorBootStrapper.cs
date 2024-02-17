using Elitetech.Academy.Data.Repository;
using Elitetech.Academy.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Elitetech.Academy.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application


            //Data
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        }
    }
}
