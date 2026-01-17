using System;
using UnityEngine;

public sealed class Singleton<T> where T : new()
{
    private static Lazy<T> _instance = new Lazy<T>(() => new T());
    public static T Instance => _instance.Value;

    private Singleton() { }
}