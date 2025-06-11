using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using ValuBakery.Application.Services;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Bootstrap.Providers.Cofigurations
{
    [ExcludeFromCodeCoverage]
    public static class AddServices
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserService, UserService>();

            // Dao
            services.AddScoped<IUserDao, UserDao>();

            return services;
        }
    }
}
