using _.WebAPI.Models;

namespace _.WebAPI.Core.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<Transaction?> GetTransactionByDbId(int id);
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByNationalId(int nationalId);
    Task<bool> GenericTransaction(Transaction transaction);
}