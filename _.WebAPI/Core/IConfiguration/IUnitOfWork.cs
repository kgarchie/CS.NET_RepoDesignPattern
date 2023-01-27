using _.WebAPI.Core.IRepository;

namespace _.WebAPI.Core.IConfiguration;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ITransactionRepository Transactions { get; }
    Task CompleteAsync();
    void SaveChanges();
}