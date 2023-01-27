using _.WebAPI.Core.IConfiguration;
using _.WebAPI.Core.IRepository;
using _.WebAPI.Core.Repository;

namespace _.WebAPI.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ModelContext _context;
    private readonly ILogger _logger;
    public IUserRepository Users { get; private set; }
    public ITransactionRepository Transactions { get; }

    public UnitOfWork(ModelContext context, ILoggerFactory logger)
    {
        _context = context;
        _logger = logger.CreateLogger("UnitOfWork");
        
        Users = new UserRepository(_context, _logger);
        Transactions = new TransactionRepository(_context, _logger);
    }
    
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}