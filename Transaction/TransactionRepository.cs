namespace Examples.UnitOfWork.Transaction
{
    using Examples.UnitOfWork;
    using Examples.UnitOfWork.Transaction.DatabaseContext;

    public class TransactionRepository : Repository
    {
        public TransactionRepository(TransactionDbContext dbContext) : base(dbContext)
        {
        }

        public void ModifyTransaction(Transaction transaction)
        {
            Add(transaction);
        }
    }
}
