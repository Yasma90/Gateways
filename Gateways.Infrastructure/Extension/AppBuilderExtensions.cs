using Gateways.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Infrastructure.Extension
{
    public static class AppBuilderExtensions
    {
        public static void AddConfigureSeedData(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                if (dbInitializer != null)
                {
                    dbInitializer.Initialize();
                    dbInitializer.SeedData();
                }
            }
        }
    }
}
