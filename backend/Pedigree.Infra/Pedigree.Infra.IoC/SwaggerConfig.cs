using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Pedigree.Infra.IoC
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pedigree API",
                    Version = "v1",
                    Description = "This is the Pedigree API. For more information, access the README.MD.",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer Scheme. \r\n\r\n Enter 'Bearer' [space] " +
                       "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference =  new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                        }
                });
            });

            return services;
        }
    }
}