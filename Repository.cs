namespace Examples.UnitOfWork
{
    public class Repository
    {
        private readonly DatabaseContext _dbContext;

        public Repository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(IBaseEntity entity)
        {
            _dbContext.Add(entity);
        }
    }
}
