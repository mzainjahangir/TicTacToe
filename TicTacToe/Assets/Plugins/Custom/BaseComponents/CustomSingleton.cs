///////////////////////////////////////////////////////////////
//
// CustomSingleton (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/6/2017
//
///////////////////////////////////////////////////////////////

using UnityEngine;

namespace Custom
{
    public abstract class CustomSingleton<T> : CustomBehaviour where T : CustomSingleton<T>
    {
        protected virtual bool DestroyNewInstances { get { return false; } }
        protected virtual bool FindInstanceIfMissing { get { return false; } }

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var existingInstance = FindObjectOfType<T>();
                    if (existingInstance != null && existingInstance.FindInstanceIfMissing)
                    {
                        _instance = existingInstance;
                    }
                    else
                    {
                        Debug.LogError(string.Format("CustomSingleton<{0}> couldn't be found.", typeof(T).Name));
                    }
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        public static bool HasInstance { get { return _instance != null; } }

        protected override void Awake()
        {
            base.Awake();

            if (_instance == null)
            {
                Instance = (T)this;
                Instance.Initialize();
            }
            else if (_instance != this)
            {
                if (DestroyNewInstances)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning(string.Format("An Singleton of type {0} already exists in the scene!", GetType()), Instance);
                }
            }
        }

        protected virtual void OnEnable()
        {
            if (_instance == null)
            {
                Instance = (T)this;
                Instance.Initialize();
            }
        }

        protected virtual void OnDestroy()
        {
            if (HasInstance && Instance == this)
            {
                Instance = null;
            }
        }

        protected virtual void Initialize() { }
    }
}
