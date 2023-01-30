namespace _.Contracts;

public record TopUpMoneyBalanceRequest(
    int UserId,
    int Amount
);