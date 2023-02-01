namespace _.Contracts;

public record BuyAirtimeRequest(
    int Amount,
    int FromUserId,
    int ToUserId,
    string PhoneNumber
);