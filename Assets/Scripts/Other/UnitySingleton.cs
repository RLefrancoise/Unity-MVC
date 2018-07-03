using UnityEngine;

namespace Other
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance ?? (_instance = FindObjectOfType<T>());

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}
