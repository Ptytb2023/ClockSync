using UnityEngine;

namespace Infrastructure.Services.Coroutines
{
    public class CoroutineRuner : MonoBehaviour
    {
        private void Awake() => 
            DontDestroyOnLoad(gameObject);
    }
}
