using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Dictionary<Type, List<Delegate>> subscribers = new Dictionary<Type, List<Delegate>>();
    
    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (!subscribers.ContainsKey(type))
        {
            subscribers[type] = new List<Delegate>();
        }
        subscribers[type].Add(callback);
    }
    
    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (subscribers.ContainsKey(type))
        {
            subscribers[type].Remove(callback);
        }
    }
    
    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (subscribers.ContainsKey(type))
        {
            foreach (var d in subscribers[type])
            {
                (d as Action<T>)?.Invoke(eventData);
            }
        }
    }
}
