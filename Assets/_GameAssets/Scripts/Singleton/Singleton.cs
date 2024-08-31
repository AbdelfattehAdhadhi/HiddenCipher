using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : class
{
    private static Singleton<T> instance;
    public bool DontDestroy;

    public static T Instance => instance as T;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            if (DontDestroy)
                DontDestroyOnLoad(this);
            Initialize();
        }
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void Shutdown()
    {
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            Shutdown();
        }
    }

    protected virtual void OnApplicationQuit()
    {
        if (instance == this)
        {
            instance = null;
            Shutdown();
        }
    }
}