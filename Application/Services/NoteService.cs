using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<List<NoteListDTO>> GetNotesAsync()
        {
            var notes = await _noteRepository.GetAllAsync();
            return notes.Select(n => new NoteListDTO
            {
                Id = n.Id,
                Title = n.Title,
                Category = n.Category
            }).ToList();
        }

        public async Task<NoteDetailDTO> GetNoteDetailAsync(Guid id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            return new NoteDetailDTO
            {
                Id = note.Id,
                Title = note.Title,
                Category = note.Category,
                Content = note.Content
            };
        }

        public async Task<Guid> AddNoteAsync(NoteInsertDTO noteDto)
        {
            var note = new TNote
            {
                Title = noteDto.Title,
                Content = noteDto.Content,
                Category = noteDto.Category
            };
            await _noteRepository.AddAsync(note);
            return note.Id;
        }

        public async Task<NoteDetailDTO> UpdateNoteAsync(Guid id, NoteUpdateDTO noteDto)
        {
            if (id != noteDto.Id)
                throw new ArgumentException("ID'ler uyuşmuyor");

            var note = await _noteRepository.GetByIdAsync(id);
            note.Title = noteDto.Title;
            note.Content = noteDto.Content;
            note.Category = noteDto.Category;

            await _noteRepository.UpdateAsync(note);

            return new NoteDetailDTO
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                Category = note.Category
            };
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            await _noteRepository.DeleteAsync(id);
        }
    }
}
