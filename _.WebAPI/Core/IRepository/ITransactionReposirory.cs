﻿using _.WebAPI.Models;

namespace _.WebAPI.Core.IRepository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetAllTransactions();
    Task<Transaction?> GetTransactionByTransactionId(string transactionId);
    Task<bool> BuyAirtime(Transaction transaction);
    Task<bool> TopUpMoneyBalance(Transaction transaction);
}