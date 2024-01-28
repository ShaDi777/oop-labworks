namespace Core.Models.Users;

public record User(
    long Id,
    string Name,
    UserRole UserRole);