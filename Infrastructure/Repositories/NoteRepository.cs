using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class NoteRepository : BaseRepository<TNote>, INoteRepository
    {
        public NoteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
