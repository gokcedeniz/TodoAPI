using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Entities

{
    public class TTodo
    {
        public TTodo()
        {
            IsDeleted = false;
            IsCompleted = false;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Priority { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }

    }

}