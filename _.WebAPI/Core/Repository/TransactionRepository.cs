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

    public async Task<bool> BuyAirtime(Transaction transaction)
    {
        Transaction airTimeTransaction = new()
        {
            FromUserId = transaction.FromUserId,
            ToUserId = transaction.ToUserId,
            TransactionAmount = transaction.TransactionAmount,
            TransactionDate = DateTime.Now,
            TransactionType = 2,
            SystemTransactionId = Guid.NewGuid().ToString()
        };
        
        try
        {
            // deduct from sender
            var sender = transaction.FromUser;
            if (sender == null) return false;

            sender.Balance -= transaction.TransactionAmount;
            Db.Entry(sender).State = EntityState.Modified;
            await Db.SaveChangesAsync();

            // add to transaction table
            await Db.Transactions.AddAsync(airTimeTransaction);
            Db.Entry(airTimeTransaction).State = EntityState.Added;
            await Db.SaveChangesAsync();

            // add to receiver as airtime
            // TODO: to be modified once db has been updated with phone numbers

            return true;
        }
        catch (Exception e)
        {
            MyLogger.LogError(e, "Error buying airtime");
            return false;
        }
    }
    
    public async Task<bool> TopUpMoneyBalance(Transaction transaction)
    {
        Transaction topUpTransaction = new()
        {
            ToUserId = transaction.FromUserId, // they are same
            FromUserId = transaction.FromUserId,
            TransactionAmount = transaction.TransactionAmount,
            TransactionDate = DateTime.Now,
            TransactionType = 1,
            SystemTransactionId = Guid.NewGuid().ToString()
        };
        
        try
        {
            // add to transaction table
            await Db.Transactions.AddAsync(topUpTransaction);
            Db.Entry(topUpTransaction).State = EntityState.Added;
            
            // add to User as money balance
            var user = await Db.Users.SingleOrDefaultAsync(x => x.DbUserId == transaction.FromUserId);
            if (user == null) return false;
            user.Balance += transaction.TransactionAmount;
            Db.Entry(user).State = EntityState.Modified;
            
            await Db.SaveChangesAsync();
            return true;
        } catch (Exception e)
        {
            MyLogger.LogError(e, "Error topping up money balance");
            return false;
        }
    }
}