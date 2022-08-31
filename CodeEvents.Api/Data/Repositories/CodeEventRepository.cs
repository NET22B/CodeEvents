using CodeEvents.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeEvents.Api.Data.Repositories
{
    public class CodeEventRepository
    {
        private readonly CodeEventsApiContext db;

        public CodeEventRepository(CodeEventsApiContext db)
        {
            this.db = db;
        }

        internal async Task<IEnumerable<CodeEvent>> GetAsync(bool includeLectures = false)
        {
            return includeLectures ? await db.CodeEvent
                            .Include(c => c.Location)
                            .Include(c => c.Lectures)
                            .ToListAsync() :
                            await db.CodeEvent
                            .Include(c => c.Location)
                            .ToListAsync();
        }
    }
}
