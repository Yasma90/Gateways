using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Gateways.Persistence.Repository;
using Gateways.Persistence.Context;
using Gateways.Persistence.UnitOfWork;
using Gateways.Persistence.UnitOfWork.Interface;
using Gateways.Persistence.Repository.Interface;
using System.Reflection;
using Gateways.Persistence;

namespace Gateways.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GatewaysDbContext>(opt => opt
                .UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(GatewaysDbContext)
                                          .GetTypeInfo().Assembly.GetName().Name)));
            services.AddTransient<IGatewayRepository, GatewayRepository>();
            services.AddTransient<IPeripheralDeviceRepository, PeripheralDeviceRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            return services;
        }

        public static IServiceCollection AddDbInitializer(this IServiceCollection services) =>
            services.AddScoped<IDbInitializer, DbInitializer>();

    }
}
