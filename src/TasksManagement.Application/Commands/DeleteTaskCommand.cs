using MediatR;
using TasksManagement.Domain.Interfaces;

namespace TasksManagement.Application.Commands
{
    public record DeleteTaskCommand(Guid id) : IRequest<bool>;
    public class DeleteTaskCommandHandler(ITasksRepository tasksRepository)
        : IRequestHandler<DeleteTaskCommand, bool>
    {
        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await tasksRepository.DeleteTaskAsync(request.id);
        }
    }
}
