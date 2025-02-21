using System.Dynamic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindFirstObjectByType<T>();

                if (null == _instance)
                {
                    GameObject gameObject = new GameObject();
                    _instance = gameObject.AddComponent<T>();
                }
            }

            return _instance;
        }

        private set { _instance = value; }
    }

    public static void CreateInstance()
    {
        _instance = Instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            CreateInstance();
        }
    }
}
