using System;
using System.Collections.Generic;

namespace _.WebAPI.Models;

public partial class Usersinfo
{
    public int? DbuserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int NationalUserId { get; set; }

    public DateTime? LastTopUp { get; set; }

    public DateTime? Joined { get; set; }

    public string? Password { get; set; }

    public virtual User? Dbuser { get; set; }
}
