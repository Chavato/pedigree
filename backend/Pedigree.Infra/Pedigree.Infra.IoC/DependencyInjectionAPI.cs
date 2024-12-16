
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pedigree.Application.Interfaces;
using Pedigree.Application.Profiles;
using Pedigree.Application.Services;
using Pedigree.Domain.Interfaces.Repositories;
using Pedigree.Infra.Data.Context;
using Pedigree.Infra.Data.Identity;
using Pedigree.Infra.Data.Profiles;
using Pedigree.Infra.Data.Repositories;

namespace Pedigree.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(InfrastructureMappingProfile), typeof(ApplicationMappingProfile));

            services.ConfigureDatabase(configuration);

            services.AddHttpContextAccessor();

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
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserInformation, UserInformation>();
            return services;
        }
    }
}