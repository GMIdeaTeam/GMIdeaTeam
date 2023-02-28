using UnityEngine;

namespace Idea.Util
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();

                        singletonObject.name = typeof(T).ToString();

                        instance = singletonObject.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        public virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);

                return;
            }

            else
            {
                instance = GetComponent<T>();
            }

            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}