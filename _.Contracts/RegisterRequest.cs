namespace _.Contracts;

public record RegisterRequest(
    string FirstName,
    string? MiddleName,
    string LastName,
    string Password,
    int NationalUserId
);