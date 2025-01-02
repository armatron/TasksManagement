using Microsoft.EntityFrameworkCore;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Interfaces;
using TasksManagement.Infrastructure.Data;

namespace TasksManagement.Infrastructure.Repositories
{
    public class TasksRepository(AppDbContext dbContext) : ITasksRepository
    {
        public async Task<IEnumerable<TasksEntity>> GetTasksAsync()
        {
            return await dbContext.Tasks.ToArrayAsync();
        }

        public async Task<TasksEntity> GetTaskByIdAsync(Guid id)
        {
            return await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TasksEntity> AddTaskAsync(TasksEntity task)
        {
            dbContext.Entry(task).State = EntityState.Added;

            task.Id = Guid.NewGuid();
            task.CreatedDate = DateTime.UtcNow;
            task.UpdatedDate = null;

            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TasksEntity> UpdateTaskAsync(Guid id, TasksEntity task)
        {
            var dbTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTask is not null)
            {
                dbContext.Entry(dbTask).State = EntityState.Modified;
                
                dbTask.Title = task.Title;
                dbTask.Description = task.Description;
                dbTask.isCompleted = task.isCompleted;
                dbTask.DueDate = task.DueDate;
                dbTask.Priority = task.Priority;
                //dbTask.CreatedDate = task.CreatedDate;
                dbTask.UpdatedDate = DateTime.UtcNow;
                
                await dbContext.SaveChangesAsync();

                return dbTask;
            }

            return null;
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var dbTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTask is not null)
            {
                dbContext.Entry(dbTask).State = EntityState.Deleted;

                dbContext.Remove(dbTask);
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
