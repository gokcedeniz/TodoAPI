using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<List<ToDoListDTO>> GetTodosAsync()
        {
            var todos = await _toDoRepository.GetAllAsync();
            var mappedList = new List<ToDoListDTO>(); //Entity'i DTO'ya ceviriyorum
            foreach (var todo in todos)
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
            return mappedList;
        }
        public async Task<ToDoDetailDTO> GetTodoDetailAsync(Guid id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);
            var mapped = new ToDoDetailDTO
            {
                Description = todo.Description,
                Id = todo.Id,
                Priority = todo.Priority,
                IsCompleted = todo.IsCompleted,
                Title = todo.Title,
            };
            return mapped;
        }

        public async Task<Guid> AddTodoAsync(ToDoInsertDTO todo)
        {
            var item = new TToDo { Title = todo.Title, Description = todo.Description, Priority = todo.Priority };
            await _toDoRepository.AddAsync(item);
            return item.Id;
        }

        public async Task MarkAsDoneAsync(Guid id)
        {
            var item = await _toDoRepository.GetByIdAsync(id);
            if (item != null)
            {
                item.IsCompleted = true;
                await _toDoRepository.UpdateAsync(item);
            }
        }

        public async Task MarkAsUnDoneAsync(Guid id)
        {
            var item = await _toDoRepository.GetByIdAsync(id);
            if (item != null)
            {
                item.IsCompleted = false;
                await _toDoRepository.UpdateAsync(item);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _toDoRepository.DeleteAsync(id);
        }

        public async Task<ToDoDetailDTO> UpdateTodoAsync(Guid todoId, ToDoupdateDTO todoDTO)
        {
            if (todoId != todoDTO.Id)
                throw new ArgumentException("Gönderilen ID'ler uyuşmuyor");

            if (todoId == null)
                throw new KeyNotFoundException("Todo bulunamadı");

            var todo = await _toDoRepository.GetByIdAsync(todoId);
            todo.Description = todoDTO.Description;
            todo.Title = todoDTO.Title;
            todo.IsCompleted = todoDTO.IsCompleted;
            todo.Priority = todoDTO.Priority;
            //try
            //{
            //    await _toDoRepository.UpdateAsync(todo);
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    return BadRequest();
            //}
            var newMapped = new ToDoDetailDTO();
            newMapped.Description = todo.Description;
            newMapped.Id = todo.Id;
            newMapped.Priority = todo.Priority;
            newMapped.IsCompleted = todo.IsCompleted;
            newMapped.Title = todo.Title;
            await _toDoRepository.UpdateAsync(todo);
            return newMapped;
        }
    }
}
