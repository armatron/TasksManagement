using TasksManagement.Domain.Entities;

namespace TasksManagement.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<TasksEntity>> GetTasksAsync();
        Task<TasksEntity> GetTaskByIdAsync(Guid id);
        Task<TasksEntity> AddTaskAsync(TasksEntity task);
        Task<TasksEntity> UpdateTaskAsync(Guid id, TasksEntity task);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
