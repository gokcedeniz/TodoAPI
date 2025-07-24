using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public TodoController(IToDoService toDoRepository)
        {
            _toDoService = toDoRepository;
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Guid>> InsertTodo(ToDoInsertDTO todoDto)
        {
            var id = await _toDoService.AddTodoAsync(todoDto);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoListDTO>>> GetTodoList()
        {
            var list = await _toDoService.GetTodosAsync();
            return Ok(list);
        }

        [HttpGet("detail/{todoId}")]
        public async Task<IActionResult> GetTodoDetail(Guid todoId)
        {
            var todo = await _toDoService.GetTodoDetailAsync(todoId);
            return Ok(todo);
        }

        [HttpPut("edit/{todoId}")]
        public async Task<IActionResult> UpdateTodo(Guid todoId, ToDoupdateDTO todoDTO) //TODO: Service katmanına ekle
        {
            //if (todoId != todoDTO.Id) return BadRequest();
            //if (!(await TodoExists(todoId))) return NotFound(); //verilen id'li todo var mı kontrol ediyorum

            //var todo = await _toDoService.Todos.FirstOrDefaultAsync(x => x.Id == todoDTO.Id);
            //todo.Description = todoDTO.Description;
            //todo.Title = todoDTO.Title;
            //todo.IsCompleted = todoDTO.IsCompleted;
            //todo.Priority = todoDTO.Priority;
            //try
            //{
            //    _toDoService.Todos.Update(todo);
            //    await _toDoService.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    return BadRequest();
            //}
            //var newMapped = new ToDoDetailDTO();
            //newMapped.Description = todo.Description;
            //newMapped.Id = todo.Id;
            //newMapped.Priority = todo.Priority;
            //newMapped.IsCompleted = todo.IsCompleted;
            //newMapped.Title = todo.Title;
            //return Ok(newMapped);
            return Ok(todoDTO);
        }

        [HttpDelete("delete/{todoId}")]
        public async Task<IActionResult> DeleteTodo(Guid todoId)
        {
            await _toDoService.DeleteAsync(todoId);
            return Ok();
        }

        //private async Task<bool> TodoExists(Guid Id)
        //{
        //    return await _toDoService.Todos.AnyAsync(x => x.Id == Id);
        //}
    }
}
