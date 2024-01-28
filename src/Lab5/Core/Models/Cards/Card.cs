namespace Core.Models.Cards;

public record Card(
    long CardNumber,
    long OwnerId)
{
    public decimal Balance { get; set; } = 0;
}