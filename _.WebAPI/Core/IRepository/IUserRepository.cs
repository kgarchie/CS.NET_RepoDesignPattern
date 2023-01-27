using _.Contracts;
using _.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace _.WebAPI.Core.IRepository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<string?> GetFullUserName(User user);
    Task<bool> RegisterUser(RegisterRequest request);
    Task<bool> TopUp(Transaction transaction);
    Task<IEnumerable<Transaction>?> GetRecentTransactions(User user, int days);
    Task<List<Usersinfo>?> FindUsersByName(string name);
    Task<List<Usersinfo>>? FindUsersByName(string? fname, string? sname, string? lname);
    Task<bool> UpdateUser(Usersinfo userInfo);
}