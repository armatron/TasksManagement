using TasksManagement.Application;
using TasksManagement.Infrastructure;

namespace TasksManagement.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddApplication()
                .AddInfrastructure();

            return services;
        }
    }
}
