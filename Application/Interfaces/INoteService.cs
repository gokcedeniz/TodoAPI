using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteListDTO>> GetNotesAsync();
        Task<NoteDetailDTO> GetNoteDetailAsync(Guid id);
        Task<Guid> AddNoteAsync(NoteInsertDTO noteDto);
        Task<NoteDetailDTO> UpdateNoteAsync(Guid id, NoteUpdateDTO noteDto);
        Task DeleteNoteAsync(Guid id);
    }
}
