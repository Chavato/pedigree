using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pedigree.Infra.Data.Context;
using Pedigree.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowAnyOrigin());
            });

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    applicationDbContext.Database.Migrate();
}

app.Run();
