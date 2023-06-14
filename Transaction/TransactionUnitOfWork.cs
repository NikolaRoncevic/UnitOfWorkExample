namespace Examples.UnitOfWork.Transaction
{
    using Examples.UnitOfWork.Transaction.DatabaseContext;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

public class TransactionUnitOfWork
{
    public TransactionRepository TransactionRepository { get; }

    public TransactionDbContext TransactionDbContext;

    public TransactionUnitOfWork(TransactionRepository transactionRepository,
                                    TransactionDbContext transactionDbContext)
    {
        TransactionRepository = transactionRepository;
        TransactionDbContext = transactionDbContext;
    }

    public Task<IEnumerable<IBaseEntity>> Save(CancellationToken cancellationToken = default)
    {
        return TransactionDbContext.Save(cancellationToken);
    }
}
}
