namespace Examples.UnitOfWork.Transaction.DatabaseContext
{
    using Examples.UnitOfWork;
    using Microsoft.Azure.Cosmos;

    public class TransactionDbContext : DatabaseContext
    {
        public TransactionDbContext(Container _userContainer) : base(_userContainer)
        {
        }
    }
}
