using MediatR;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Interfaces;

namespace TasksManagement.Application.Commands
{
    public record AddTaskCommand(TasksEntity task) : IRequest<TasksEntity>;

    public class AddTaskCommandHandler(ITasksRepository tasksRepository)
        : IRequestHandler<AddTaskCommand, TasksEntity>
    {
        public async Task<TasksEntity> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            return await tasksRepository.AddTaskAsync(request.task);
        }
    }
}
