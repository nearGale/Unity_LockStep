using System;

public abstract class Singleton<T> : IDisposable where T : class, new()
{
    private static T _instance;
    protected static bool hasCreated = false;

    public static T Instance => _instance;

    public static T Create(T value = null)
    {
        if (!hasCreated)
        {
            _instance = value ?? new();
            hasCreated = true;
        }

        return _instance;
    }

    public virtual void Dispose()
    {
        hasCreated = false;
        _instance = null;
    }
}
