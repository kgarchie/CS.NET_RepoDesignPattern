using _.WebAPI.Core.IRepository;
using _.WebAPI.Data;
using _.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace _.WebAPI.Core.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ModelContext Db;
    protected readonly ILogger MyLogger;

    protected GenericRepository(ModelContext db, ILogger myLogger)
    {
        Db = db;
        MyLogger = myLogger;
    }

    public async Task<Transaction?> GetTransactionByDbId(int id)
    {
        return await Db.Transactions.Where(x => x.DbTransactionId == id).SingleOrDefaultAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await Db.Users.Where(x => x.DbUserId == id).SingleOrDefaultAsync();
    }
    
    public async Task<User?> GetUserByNationalId(int nationalId)
    {
        var userInfo = await Db.Usersinfos.SingleOrDefaultAsync(x => x.NationalUserId == nationalId);
        if (userInfo == null) return null;

        var user = await Db.Users.SingleOrDefaultAsync(x => x.DbUserId == userInfo.DbuserId);

        return user;
    }

    public async Task<bool> MakeTransaction(Transaction transaction)
    {
        try
        {
            // deduct from sender
            var sender = transaction.FromUser;
            if (sender == null) return false;

            sender.Balance -= transaction.TransactionAmount;
            Db.Entry(sender).State = EntityState.Modified;
            await Db.SaveChangesAsync();

            // add to transaction table
            await Db.Transactions.AddAsync(transaction);
            Db.Entry(transaction).State = EntityState.Added;
            await Db.SaveChangesAsync();

            // add to receiver
            var receiver = transaction.ToUser;
            if (receiver == null) return false;

            receiver.Balance += transaction.TransactionAmount;
            Db.Entry(receiver).State = EntityState.Modified;

            await Db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            MyLogger.LogError(ex, "{Repo} Make Transactional Error", typeof(GenericRepository<T>).Name);
            return false;
        }
    }
}