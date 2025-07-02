using System;
using System.Collections.Generic;

namespace Roguely.Core;

public static class Global
{
    private static Dictionary<Type, object> _globalObjects = new();

    public static void Register<T>(T obj) where T : class
    {
        _globalObjects[typeof(T)] = obj;
    }

    public static T GetInstance<T>() where T : class
    {
        if (_globalObjects.TryGetValue(typeof(T), out var obj))
        {
            return obj as T;
        }
        return null;
    }
}