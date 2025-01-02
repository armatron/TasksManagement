using MediatR;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Interfaces;

namespace TasksManagement.Application.Queries
{
    public record GetTaskByIdQuery(Guid id) : IRequest<TasksEntity>;
    public class GetTaskByIdQueryHandler(ITasksRepository tasksRepository)
        : IRequestHandler<GetTaskByIdQuery, TasksEntity>
    {
        public async Task<TasksEntity> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTaskByIdAsync(request.id);
        }
    }
}
