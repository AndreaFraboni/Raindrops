using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monosingleton<T> : MonoBehaviour where T : Monosingleton<T>
{
    private static T instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Instance of the singleton " + (T)this + "already exists, deleting");
            Destroy(instance);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = (T)this;
        }
    }

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

}
