using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksManagement.Application.Commands;
using TasksManagement.Application.Queries;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Api.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TasksController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddTaskAsync([FromBody] TasksEntity task)
        {
            var result = await sender.Send(new AddTaskCommand(task));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksAsync()
        {
            var result = await sender.Send(new GetTasksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] Guid id)
        {
            var result = await sender.Send(new GetTaskByIdQuery(id));
            
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] Guid id, [FromBody] TasksEntity task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var result = await sender.Send(new UpdateTaskCommand(id, task));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id)
        {
            var result = await sender.Send(new DeleteTaskCommand(id));

            if (result != true)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
