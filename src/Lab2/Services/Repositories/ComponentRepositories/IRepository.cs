namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public interface IRepository<T>
{
    void Create(T component);
    T? FindByName(string name);
    void Update(T component);
    bool DeleteByName(string name);
}