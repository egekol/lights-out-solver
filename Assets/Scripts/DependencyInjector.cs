

using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class DependencyInjector : Singleton<DependencyInjector>
{

    private readonly Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

    public void Register<T>(T dependency)
    {
        _dependencies[typeof(T)] = dependency;
    }

    public T Resolve<T>()
    {
        return (T) Resolve(typeof(T));
    }

    private object Resolve(Type type)
    {
        if (_dependencies.TryGetValue(type, out var dependency))
        {
            return dependency;
        }

        Debug.LogError($"'{type}' not found.");
        // Debug.LogError("Null");
        return null;
    }
}