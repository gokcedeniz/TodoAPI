namespace Application.Models
{
    public class ToDoListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted {  get; set; }
        public int Priority {  get; set; }

    }
    public class ToDoDetailDTO : ToDoListDTO
    {
        public string Description { get; set; }
    }
    public class ToDoupdateDTO : ToDoListDTO
    {
        public string Description { get; set; }
    }

    public class ToDoInsertDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
