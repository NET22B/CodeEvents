using CodeEvents.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeEvents.Api.Data.Repositories
{
    public class LectureRepository
    {

        private readonly CodeEventsApiContext db;

        public LectureRepository(CodeEventsApiContext db)
        {
            this.db = db;
        }

        internal async Task<IEnumerable<Lecture>> GetLecturesForEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return await db.Lecture.Where(l => l.CodeEvent.Name == name).ToListAsync();
        }
    }
}
