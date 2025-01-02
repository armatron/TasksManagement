using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksManagement.Domain.Entities;
using TasksManagement.Infrastructure.Data;

namespace TasksManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class task : ControllerBase
    {
        private readonly AppDbContext _context;

        public task(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TasksDomain
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksEntity>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/TasksDomain/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksEntity>> GetTasksEntity(int id)
        {
            var tasksEntity = await _context.Tasks.FindAsync(id);

            if (tasksEntity == null)
            {
                return NotFound();
            }

            return tasksEntity;
        }

        // PUT: api/TasksDomain/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksEntity(int id, TasksEntity tasksEntity)
        {
            if (id != tasksEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(tasksEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TasksDomain
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasksEntity>> PostTasksEntity(TasksEntity tasksEntity)
        {
            _context.Tasks.Add(tasksEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasksEntity", new { id = tasksEntity.Id }, tasksEntity);
        }

        // DELETE: api/TasksDomain/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksEntity(int id)
        {
            var tasksEntity = await _context.Tasks.FindAsync(id);
            if (tasksEntity == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasksEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksEntityExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
