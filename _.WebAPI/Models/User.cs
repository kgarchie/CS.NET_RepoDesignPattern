using System;
using System.Collections.Generic;

namespace _.WebAPI.Models;

public partial class User
{
    public int DbUserId { get; set; }

    public int Balance { get; set; }

    public sbyte AccountStatus { get; set; }

    public virtual ICollection<Transaction> TransactionFromUsers { get; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionToUsers { get; } = new List<Transaction>();

    public virtual ICollection<Usersinfo> Usersinfos { get; } = new List<Usersinfo>();
}
