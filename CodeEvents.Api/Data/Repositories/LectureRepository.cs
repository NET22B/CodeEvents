namespace CodeEvents.Api.Data.Repositories
{
    public class LectureRepository
    {

        private readonly CodeEventsApiContext db;

        public LectureRepository(CodeEventsApiContext db)
        {
            this.db = db;
        }

    }
}
