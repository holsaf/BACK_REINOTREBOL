using System.Runtime.CompilerServices;

namespace ReinoTrebolApi.Extensiones
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
    using ReinoTrebolApi.Models.Data.DbContext;
    using ReinoTrebolApi.Repository.Solicitud;

    public static class ServiceCollectionExtensions
    {
        public static void AddReinoTrebolDbContext (this IServiceCollection collection, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
            collection.AddDbContext<IReinotrebolDbContext, ReinoTrebolDbContext>(options => options
            .UseMySql(connectionString, serverVersion, b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}_{entity}"))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());
        }

        public static void BuidDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<ReinoTrebolDbContext>();
            dataContext.Database.Migrate();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISolicitudRepository, SolicitudRepository>();
            services.AddScoped<ISolicitudService, SolicitudService>();
            // Configuracion de Auto Mapper 
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RepositoryMappingProfile());
                mc.AddProfile(new ServiceMappingProfile());

            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
