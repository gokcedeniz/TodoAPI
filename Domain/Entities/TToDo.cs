using Domain.Entities.Base;

namespace Domain.Entities

{
    public class TToDo : TBase
    {
        public TToDo()
        {
            IsCompleted = false;
        }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Priority { get; set; }
        public bool IsCompleted { get; set; }

    }

}