namespace TestTask.Persistence
{
    public class DbInitializer
    {
        public static void DbInitialize(FileApplicationDbContext fileApplicationDbContext) 
            => fileApplicationDbContext.Database.EnsureCreated();
    }
}
