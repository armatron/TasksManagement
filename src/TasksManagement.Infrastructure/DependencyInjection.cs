using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TasksManagement.Domain.Interfaces;
using TasksManagement.Infrastructure.Data;
using TasksManagement.Infrastructure.Repositories;

namespace TasksManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // set inMemory database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TasksManagement");
            });

            services.AddScoped<ITasksRepository, TasksRepository>();

            return services;
        }
    }
}
