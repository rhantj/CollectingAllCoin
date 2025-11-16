using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    private static T inst;

    public static T Instance
    {
        get
        {
            if (inst == null)
            {
                inst = (T)FindFirstObjectByType(typeof(T));

                if (inst = null)
                {
                    var go = new GameObject();
                    inst = go.AddComponent<T>();

                    go.name = typeof(T).Name;

                    DontDestroyOnLoad(go);
                }
            }
            return inst;
        }
    }

    protected virtual void Awake()
    {
        if (inst == null)
        {
            inst = this as T;
            DontDestroyOnLoad(inst);
        }
        else Destroy(gameObject);
    }
}