namespace _.Contracts;

public record MakeTransactionRequest(
    int TransactionAmount,
    int UserFromId,
    int UserToId
    );