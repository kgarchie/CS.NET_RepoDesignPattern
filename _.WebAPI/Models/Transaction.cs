using System;
using System.Collections.Generic;

namespace _.WebAPI.Models;

public partial class Transaction
{
    public int DbTransactionId { get; set; }

    public sbyte? TransactionType { get; set; }

    public int TransactionAmount { get; set; }

    public int? FromUserId { get; set; }

    public int? ToUserId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? SystemTransactionId { get; set; }

    public virtual User? FromUser { get; set; }

    public virtual User? ToUser { get; set; }
}
