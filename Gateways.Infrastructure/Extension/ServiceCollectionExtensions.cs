using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Gateways.Persistence.Repository;
using Gateways.Persistence.DbContext;
using Gateways.Persistence.UnitOfWork;
using Gateways.Persistence.UnitOfWork.Interface;
using Gateways.Persistence.Repository.Interface;
using System.Reflection;

namespace Gateways.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IGatewayRepository, GatewayRepository>();
            services.AddTransient<IPeripheralDeviceRepository, PeripheralDeviceRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<GatewaysDbContext>(opt => opt
                .UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(GatewaysDbContext)
                                          .GetTypeInfo().Assembly.GetName().Name)));

            return services;
        }

    }
}
