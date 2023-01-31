using _.Contracts;
using _.WebAPI.Core.IRepository;
using _.WebAPI.Data;
using _.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace _.WebAPI.Core.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ModelContext db, ILogger myLogger) : base(db, myLogger)
    {
    }
    
    public async Task<bool> RegisterUser(RegisterRequest request)
    {
        try
        {
            /* This repository function performs the actual user registration by doing the dirty db work
             * Returns true or false depending on whether registration was successful
             * Areas of improvement have been noted in the controller
             */
            var checkIfUserExists = await Db.Usersinfos
                .AnyAsync(x => x.NationalUserId == request.NationalUserId);

            if (checkIfUserExists) return false;
            
            // Initialising and mapping values -- There is probably a library or class in C# that does this that I am too inept to know about
            var user = new User
            {
                AccountStatus = 1,
                Balance = 0
            };
            
            var userInfo = new Usersinfo
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                NationalUserId = request.NationalUserId,
                Password = request.Password,
                Joined = DateTime.Now,
                LastTopUp = null,
                Dbuser = user
            };

            await Db.Users!.AddAsync(user);
            await Db.Usersinfos!.AddAsync(userInfo);
            
            await Db.SaveChangesAsync();
            return true;
        } catch (Exception ex)
        {
            MyLogger.LogError(ex, "{Repo} Registration Error", nameof(UserRepository));
            return false;
        }
    }

    public async Task<string?> GetFullUserName(User user)
    {
        // Gets username from db using the user object
        // Improvements can be made to also get name from user id only - simple implementation
        var userinfo = await Db.Usersinfos.FindAsync(user.DbUserId);
        if (userinfo == null) return null;
        return userinfo.FirstName + " " + userinfo.LastName;
    }

    public async Task<bool> TopUp(Transaction transaction)
    {
        // Performs a top up which in this case is just a self addition operation,
        // where the user's balance is updated with the passed value in the transaction object
        Db.Transactions.Add(transaction);
        var user = transaction.FromUser;
        if (user == null) return false;
        user.Balance += transaction.TransactionAmount;
        return await Db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Transaction>?> GetRecentTransactions(User user, int days)
    {
        // Gets the recent transactions
        if (days <= 0) return null;
        
        var transactions = await Db.Transactions
            .Where(x => x.FromUserId == user.DbUserId && x.TransactionDate >= DateTime.Now.AddDays(-days))
            .ToListAsync();
        return transactions;
    }

    public async Task<List<Usersinfo>?> FindUsersByName(string name)
    {
        // Finds a user by the one name
        var users = await Db.Usersinfos
            .Where(x => x.FirstName.Contains(name) || x.MiddleName!.Contains(name) || x.LastName.Contains(name))
            .ToListAsync();
        return users;
    }

    public Task<List<Usersinfo>>? FindUsersByName(string? fname, string? sname, string? lname)
    {
        // Finds a user by one or more names... Need improvement, I see some logical oversights.
        var users = Db.Usersinfos?.Where(x =>
                x.FirstName.Contains(fname!) || x.MiddleName!.Contains(sname!) || x.LastName.Contains(lname!))
            .ToListAsync();
        return users;
    }

    public async Task<bool> UpdateUser(Usersinfo userInfo)
    {
        // Updates a user's information
        var existingUser = await Db.Users.Where(x => userInfo.Dbuser != null && x.DbUserId == userInfo.Dbuser.DbUserId).SingleOrDefaultAsync();
        if (existingUser == null)
        {
            RegisterRequest registerRequest = new(userInfo.FirstName, userInfo.MiddleName, userInfo.LastName, userInfo.Password!,
                userInfo.NationalUserId);
            return await RegisterUser(registerRequest);
        }

        // TODO: Implement changing user account status
        // Not implemented yet because the way I'd do it will break the function. Find a fast way that will not have huge time and space complexity
        
        Db.Entry(existingUser).State = EntityState.Modified;
        await Db.SaveChangesAsync();

        var existingUserInfo = await Db.Usersinfos.SingleOrDefaultAsync(x => x.Dbuser!.DbUserId == existingUser.DbUserId);
        if (existingUserInfo == null) return false;
        
        // Maybe use lambda expressions or whatever those one liner if checks are called to make this less intellectually deprived
        if (userInfo.FirstName != string.Empty) existingUserInfo.FirstName = existingUserInfo.FirstName;
        if (userInfo.LastName != string.Empty) existingUserInfo.LastName = userInfo.LastName;
        if (userInfo.MiddleName != string.Empty) existingUserInfo.MiddleName = userInfo.MiddleName;
        if (userInfo.Password != string.Empty) existingUserInfo.Password = userInfo.Password;
        if (userInfo.NationalUserId != 0) existingUserInfo.NationalUserId = userInfo.NationalUserId;

        Db.Entry(existingUserInfo).State = EntityState.Modified;
        await Db.SaveChangesAsync();
        
        return true;
    }
}