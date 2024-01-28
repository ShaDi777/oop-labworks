using Core.Models.Cards;
using Core.Models.Users;

namespace Core.Contracts.CurrentState;

internal class CurrentStateManager : ICurrentStateService
{
    public User? User { get; set; }
    public Card? Card { get; set; }
}