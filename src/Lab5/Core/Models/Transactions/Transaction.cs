namespace Core.Models.Transactions;

public record Transaction
(
    long CardNumber,
    TransactionType TransactionType,
    decimal Value,
    DateTime TimeStamp);
