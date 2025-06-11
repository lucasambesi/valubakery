using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ValuBakery.Percistence.Contexts;

namespace ValuBakery.Bootstrap.Providers.Extensions
{
    public static class PersistenceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseContext>(
            options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(BaseContext).Assembly.FullName)));
        }

        public static IApplicationBuilder RunMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetRequiredService<BaseContext>();

                #if !DEBUG
                    context.Database.Migrate();
                #endif

            }
            return app;
        }
    }
}
