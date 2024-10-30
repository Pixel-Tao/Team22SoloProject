using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ManagerSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    private static void Init()
    {
        if (instance == null)
        {
            T manager = FindObjectOfType<T>();
            if (manager == null)
            {
                manager = new GameObject { name = nameof(T) }.AddComponent<T>();
                instance = manager;
                DontDestroyOnLoad(manager.gameObject);
            }
        }
    }
}