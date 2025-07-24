using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.DataAccess;
using TodoList.Entities;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Guid>> InsertTodo(ToDoInsertDTO todoDto)
        {
            var todo = new TTodo
            {
                Description = todoDto.Description,
                Priority=todoDto.Priority,
                Title=todoDto.Title,

            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return Ok(todoDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoListDTO>>> GetTodoList()
        {
            var List = _context.Todos; //IQueryable'ı tanımlıyorum
            var todoListTemp = List.Where(x => x.IsDeleted == false); //filtreleme ekliyoruz

            var list = await todoListTemp.ToListAsync(); //IQueryable'dan IEnumerable'a cevirir (Veritabanı sorgusu burda)
            var mappedList = new List<ToDoListDTO>(); //Entity'i DTO'ya ceviriyorum
            foreach (var todo in list)
            {
                var mapped = new ToDoListDTO
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    IsCompleted = todo.IsCompleted,
                    Priority = todo.Priority,
                };
                mappedList.Add(mapped);

                //mappedList.Add(new ToDoListDTO
                //{
                //    Id = todo.Id,
                //    Title = todo.Title,
                //    IsCompleted = todo.IsCompleted,
                //    Priority = todo.Priority,
                //});
            }

            return Ok(mappedList);

            //var todoListTemp = await _context.Todos.ToListAsync(); //IEnumerable olarak veriyi çekiyoruz(Veritabanı sorgusu burda)

            //var list = todoListTemp.Where(x => x.IsDeleted == false).ToList(); //çektiğimiz veriyi filtreliyoruz
            //var mappedList = new List<ToDoListDTO>(); //Entity'i DTO'ya ceviriyorum
            //foreach (var todo in list)
            //{
            //    var mapped = new ToDoListDTO
            //    {
            //        Id = todo.Id,
            //        Title = todo.Title,
            //        IsCompleted = todo.IsCompleted,
            //        Priority = todo.Priority,
            //    };
            //    mappedList.Add(mapped);

            //    //mappedList.Add(new ToDoListDTO
            //    //{
            //    //    Id = todo.Id,
            //    //    Title = todo.Title,
            //    //    IsCompleted = todo.IsCompleted,
            //    //    Priority = todo.Priority,
            //    //});
            //}

            //return Ok(mappedList);

        }

        [HttpGet("detail/{todoId}")]
        public async Task<IActionResult> GetTodoDetail(Guid todoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == todoId);

            if (todo == null) return NotFound();

            var mapped = new ToDoDetailDTO
            {
                Description = todo.Description,
                Id = todo.Id,
                Priority = todo.Priority,
                IsCompleted = todo.IsCompleted,
                Title = todo.Title,
            };


            return Ok(mapped);
        }

        [HttpPut("edit/{todoId}")]
        public async Task<IActionResult> UpdateTodo(Guid todoId, ToDoupdateDTO todoDTO)
        {
            if (todoId != todoDTO.Id) return BadRequest();
            if (!(await TodoExists(todoId))) return NotFound(); //verilen id'li todo var mı kontrol ediyorum

            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == todoDTO.Id);
            todo.Description = todoDTO.Description;
            todo.Title = todoDTO.Title;
            todo.IsCompleted = todoDTO.IsCompleted;
            todo.Priority = todoDTO.Priority;
            try
            {
                _context.Todos.Update(todo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            var newMapped = new ToDoDetailDTO();
            newMapped.Description = todo.Description;
            newMapped.Id = todo.Id;
            newMapped.Priority = todo.Priority;
            newMapped.IsCompleted = todo.IsCompleted;
            newMapped.Title = todo.Title;
            return Ok(newMapped);
        }

        [HttpDelete("delete/{todoId}")]
        public async Task<IActionResult> DeleteTodo(Guid todoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == todoId);
            if (todo == null) return NotFound();
            todo.IsDeleted = true;
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> TodoExists(Guid Id)
        {
            return await _context.Todos.AnyAsync(x => x.Id == Id);
        }
    }
}

//C +
//R (General(+) + Detail(+))
//U +
//D +

