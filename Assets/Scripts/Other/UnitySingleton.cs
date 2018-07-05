using UnityEngine;

namespace Other
{
    /// <inheritdoc />
    /// <summary>
    /// Singleton for Unity component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Instance of the singleton
        /// </summary>
        private static T _instance;

        /// <summary>
        /// Instance accessor
        /// </summary>
        public static T Instance => _instance ?? (_instance = FindObjectOfType<T>());

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}
