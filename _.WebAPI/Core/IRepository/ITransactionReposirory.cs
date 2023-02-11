using _.WebAPI.Models;

namespace _.WebAPI.Core.IRepository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetAllTransactions();
    Task<IEnumerable<Transaction>> GetRecentTransactions(int? userId);
    Task<Transaction?> GetTransactionByTransactionId(string transactionId);
    Task<bool> BuyAirtime(Transaction transaction);
    Task<bool> TopUpMoneyBalance(Transaction transaction);
}