using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Guid>> InsertNote(NoteInsertDTO noteDto)
        {
            var id = await _noteService.AddNoteAsync(noteDto);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteListDTO>>> GetNoteList()
        {
            var list = await _noteService.GetNotesAsync();
            return Ok(list);
        }

        [HttpGet("detail/{noteId}")]
        public async Task<IActionResult> GetNoteDetail(Guid noteId)
        {
            var note = await _noteService.GetNoteDetailAsync(noteId);
            return note == null ? NotFound() : Ok(note);
        }

        [HttpPut("edit/{noteId}")]
        public async Task<IActionResult> UpdateNote(Guid noteId, NoteUpdateDTO noteDto)
        {
            var updated = await _noteService.UpdateNoteAsync(noteId, noteDto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("delete/{noteId}")]
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            await _noteService.DeleteNoteAsync(noteId);
            return Ok();
        }
    }
}
