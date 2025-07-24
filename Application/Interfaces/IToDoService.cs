using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IToDoService
    {
        Task<List<ToDoListDTO>> GetTodosAsync();
        Task<ToDoDetailDTO> GetTodoDetailAsync(Guid id);
        Task<Guid> AddTodoAsync(ToDoInsertDTO todo);
        Task MarkAsDoneAsync(Guid id);
        Task MarkAsUnDoneAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
