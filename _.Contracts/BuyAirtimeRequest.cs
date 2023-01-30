namespace _.Contracts;

public record BuyAirtimeRequest(
    int UserId,
    int Amount,
    int FromUserId,
    int ToUserId,
    string PhoneNumber
);