using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

/* 
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
 */



//✨One of the primary and easy methods to provide reference dependency in a script can be the Singleton pattern.

//✨Although making both behaviours instances of each other can easily make them find each other,
//it can become a problem in the future as the number of behaviours increases, and each one becomes a singleton.
//
//✨It will be difficult to change one behaviour with another in the same function due to the non-modular nature of the code.
//✨Creating a reference within each cyclic dependency again (n * (n-1)) will become more challenging.

/*Instead, using a Singleton pattern along with a centralized Manager that
 handles references to all MonoBehaviour classes that need to communicate between scenes can be a better approach.
 This Manager class will be persistent across different scenes and provide an easy way to access and manage these MonoBehaviours.
*/


public class SomeBehaviour : MonoBehaviour
{
    public MonoBehaviour OtherBehaviour;

    private void Awake()
    {
        // Create a BehaviourRegistry class that will be a Singleton and persist across different scenes.

        BehaviourRegistry.Instance.RegisterBehaviour("SomeBehaviour",this);
        
    }

    private void Start()
    {

        OtherBehaviour = BehaviourRegistry.Instance.GetBehaviour<MonoBehaviour>("OtherBehaviour");

        if (OtherBehaviour!=null)
        {
            // Operations dependent on OtherBehaviour
        }
        
        
        //This solution allows you to register any number of MonoBehaviour classes with unique keys in the BehaviourRegistry.
        //You can easily retrieve the registered behaviours in any scene using the Singleton BehaviourRegistry.
        
            // Generally I use registration for unique manager classes that also may not be Mono class.
        // DI makes things
        //     a) Easily unit testable 
        //     b) Easily implementation swappable

    }
}


// Will be responsible for registering and providing access to types that need to communicate between scenes.
public class BehaviourRegistry : Singleton<BehaviourRegistry>
{
    private Dictionary<string, MonoBehaviour> registeredBehaviours = new Dictionary<string, MonoBehaviour>();

    public void RegisterBehaviour(string key, MonoBehaviour behaviour)
    {
        if (!registeredBehaviours.ContainsKey(key))
        {
            registeredBehaviours.Add(key, behaviour);
        }
        else
        {
            Debug.LogError($"Behaviour with key '{key}' is already registered.");
        }
    }

    public T GetBehaviour<T>(string key) where T : MonoBehaviour
    {
        if (registeredBehaviours.ContainsKey(key))
        {
            return registeredBehaviours[key] as T;
        }

        Debug.LogError($"Behaviour with key '{key}' not found.");
        return null;
    }
}