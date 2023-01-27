namespace _.Contracts;

public record BuyAirtimeRequest(
    int UserId,
    int Amount,
    string PhoneNumber
);