using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{

    public class NoteListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }

    public class NoteDetailDTO : NoteListDTO
    {
        public string Content { get; set; }
    }

    public class NoteInsertDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
    }

    public class NoteUpdateDTO : NoteInsertDTO
    {
        public Guid Id { get; set; }
    }
}

