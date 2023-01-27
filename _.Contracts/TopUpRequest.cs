namespace _.Contracts;

public record TopUpRequest(
    int UserId,
    int Amount
);