namespace CodeEvents.Api.Data.Repositories
{
    public class UnitOfWork
    {
        private readonly CodeEventsApiContext db;

        public CodeEventRepository CodeEventRepository { get; }
        public LectureRepository LectureRepository { get; }

        public UnitOfWork(CodeEventsApiContext db)
        {
            CodeEventRepository = new CodeEventRepository(db);
            LectureRepository = new LectureRepository(db);
            this.db = db;
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
