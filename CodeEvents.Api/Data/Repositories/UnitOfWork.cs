namespace CodeEvents.Api.Data.Repositories
{
    public class UnitOfWork
    {
        private readonly CodeEventsApiContext db;

        public CodeEventRepository CodeEventRepository { get; }
        public UnitOfWork(CodeEventsApiContext db)
        {
            CodeEventRepository = new CodeEventRepository(db);
            this.db = db;
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
