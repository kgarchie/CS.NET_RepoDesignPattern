namespace _.Contracts;

public record UpdateRequest(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    int? NationalUserId,
    byte? AccountStatus
);