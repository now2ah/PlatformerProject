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

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
    }
}
