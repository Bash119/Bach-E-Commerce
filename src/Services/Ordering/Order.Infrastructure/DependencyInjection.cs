

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Data;
using Order.Infrastructure.Interceptors;
using Ordering.Application.Data;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        // Add services to the container.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp,options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}
