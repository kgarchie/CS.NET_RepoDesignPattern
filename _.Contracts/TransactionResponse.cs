namespace _.Contracts;

public record TransactionResponse(
    int DbTransactionId,
    sbyte? TransactionType,
    int TransactionAmount,
    string FromUserName,
    string ToUserName,
    DateTime? TransactionDate,
    string? SystemTransactionId,
    string? TransactionStatus
);