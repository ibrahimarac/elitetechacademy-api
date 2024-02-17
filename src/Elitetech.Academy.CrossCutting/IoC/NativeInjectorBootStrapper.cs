using Elitetech.Academy.Data.Repository;
using Elitetech.Academy.Data.Repository.Base;
using Elitetech.Academy.Domain.Repository;
using Elitetech.Academy.Domain.Repository.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Elitetech.Academy.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application


            //Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        }
    }
}
