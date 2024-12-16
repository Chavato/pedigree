
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pedigree.Application.Profiles;
using Pedigree.Infra.Data.Context;
using Pedigree.Infra.Data.Identity;
using Pedigree.Infra.Data.Profiles;

namespace Pedigree.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(InfrastructureMappingProfile), typeof(ApplicationMappingProfile));

            services.ConfigureDatabase(configuration);

            services.AddDependencyInjectionRepositories();
            services.AddDependencyInjectionServices();

            return services;
        }

        private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is not setted.");
            }

            services.AddDbContext<ApplicationDbContext>(opt =>
                                                            opt.UseSqlServer(connectionString,
                                                                             b => b.MigrationsAssembly(typeof(ApplicationDbContext)
                                                                                                            .Assembly
                                                                                                            .FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddDependencyInjectionRepositories(this IServiceCollection services)
        {


            return services;
        }

        private static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
        {

            return services;
        }
    }
}