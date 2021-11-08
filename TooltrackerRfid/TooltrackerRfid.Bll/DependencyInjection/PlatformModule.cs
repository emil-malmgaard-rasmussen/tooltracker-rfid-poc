using Autofac;
using Microsoft.Extensions.Configuration;
using Smoerfugl.Database.Postgres;
using TooltrackerRfid.Database;

namespace TooltrackerRfid.Bll.DependencyInjection
{
    public class PlatformModule : Module
    {
        private readonly IConfiguration _configuration;

        public PlatformModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var dbSettings = new DatabaseSettings();
            _configuration.Bind("Postgres", dbSettings);
            builder.UsePostgres<TooltrackerRfidDbContext>(dbSettings);
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces();
        }
    }
}
