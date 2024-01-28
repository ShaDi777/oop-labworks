using Core.Models.Cards;
using Core.Models.Users;

namespace Core.Contracts.CurrentState;

public interface ICurrentStateService
{
    User? User { get; }
    Card? Card { get; }
}