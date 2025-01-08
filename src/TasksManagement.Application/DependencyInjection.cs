using Microsoft.Extensions.DependencyInjection;

namespace TasksManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // register MediatR (https://www.nuget.org/packages/mediatr/)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}
