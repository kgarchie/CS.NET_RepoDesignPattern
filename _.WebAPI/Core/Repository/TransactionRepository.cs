using _.WebAPI.Core.IRepository;
using _.WebAPI.Data;
using _.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace _.WebAPI.Core.Repository;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ModelContext db, ILogger myLogger) : base(db, myLogger)
    {
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactions()
    {
        return await Db.Transactions.OrderByDescending(x => x.TransactionDate).ToListAsync();
    }

    public async Task<Transaction?> GetTransactionByTransactionId(string transactionId)
    {
        return await Db.Transactions.Where(x => x.SystemTransactionId == transactionId).SingleOrDefaultAsync();
    }
}