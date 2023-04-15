using System;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 
 * The SomeBehaviour class below holds a serialized reference to another MonoBehaviour which is in the same scene.
 * On Start, it performs some operations that are dependent on this OtherBehaviour.
 * 
 * Later in development, these two behaviours were separated and were moved to different scenes.
 * The dependency still remains, therefore SomeBehaviour class should somehow get the reference of OtherBehaviour.
 * In the future, there can be more behaviours that are dependent each other. There can be cyclic dependencies in some cases.
 * 
 * Implement your simple and generalized solution to this problem, so that similar problems that can be encountered in the future can be solved easily. 
 * 
 * Make sure you describe your code and intentions clearly.
 * 
 */

// DI makes things
//     a) Easily unit testable
//     b) Easily implementation swappable


public class SomeBehaviour : MonoBehaviour
{
    public MonoBehaviour OtherBehaviour;

    private void Start()
    {
        // Operations dependent on OtherBehaviour
    }
}


public class DependencyInjector : MonoBehaviour
{
    private static DependencyInjector _instance;

    public static DependencyInjector Instance
    {
        get
        {
            // lock (_lock)
            // {
            if (_instance == null) _instance = (DependencyInjector) FindObjectOfType(typeof(DependencyInjector));
            return _instance;
            // }
        }
    }

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

        Debug.LogError("Null");
        return null;
    }
}
