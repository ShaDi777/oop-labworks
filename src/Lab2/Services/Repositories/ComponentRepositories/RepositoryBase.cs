using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class RepositoryBase<T> : IRepository<T>
    where T : IComponent
{
    private readonly Dictionary<string, T> _componentDictionary;

    public RepositoryBase(Dictionary<string, T> componentDictionary)
    {
        _componentDictionary = new Dictionary<string, T>();
        if (componentDictionary is null) return;
        foreach (string key in componentDictionary.Keys)
        {
            _componentDictionary.Add(key.ToLower(System.Globalization.CultureInfo.CurrentCulture), componentDictionary[key]);
        }
    }

    public void Create(T component)
    {
        _componentDictionary.Add(component.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture), component);
    }

    public T? FindByName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        if (!_componentDictionary.ContainsKey(name.ToLower(System.Globalization.CultureInfo.CurrentCulture)))
        {
            return default;
        }

        return _componentDictionary[name.ToLower(System.Globalization.CultureInfo.CurrentCulture)];
    }

    public void Update(T component)
    {
        if (!_componentDictionary.ContainsKey(component.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture)))
        {
            return;
        }

        _componentDictionary[component.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture)] = component;
    }

    public bool DeleteByName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _componentDictionary.Remove(name.ToLower(System.Globalization.CultureInfo.CurrentCulture));
    }
}