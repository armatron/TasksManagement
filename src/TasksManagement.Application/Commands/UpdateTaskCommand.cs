using MediatR;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Interfaces;

namespace TasksManagement.Application.Commands
{
    public record UpdateTaskCommand(Guid id, TasksEntity task) : IRequest<TasksEntity>;
    public class UpdateTaskCommandHandler(ITasksRepository tasksRepository)
        : IRequestHandler<UpdateTaskCommand, TasksEntity>
    {
        public async Task<TasksEntity> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            return await tasksRepository.UpdateTaskAsync(request.id, request.task);
        }
    }
}
