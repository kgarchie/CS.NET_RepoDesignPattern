namespace _.Contracts;

public record UpdateUserInfoRequest(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    int? NationalUserId,
    byte? AccountStatus
);