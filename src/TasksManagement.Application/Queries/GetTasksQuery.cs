using MediatR;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Interfaces;

namespace TasksManagement.Application.Queries
{
    public record GetTasksQuery() : IRequest<IEnumerable<TasksEntity>>;

    public class GetTasksQueryHandler(ITasksRepository tasksRepository)
        : IRequestHandler<GetTasksQuery, IEnumerable<TasksEntity>>
    {
        public async Task<IEnumerable<TasksEntity>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTasksAsync();
        }
    }
}
