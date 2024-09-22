using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.Coroutines
{
    public class CoroutineService : ICoroutineService
    {
        private CoroutineRuner _coroutineRuner;

        public CoroutineService(CoroutineRuner coroutineRuner) =>
            _coroutineRuner = coroutineRuner;

        public Coroutine StartCoroutine(IEnumerator coroutine) =>
            _coroutineRuner.StartCoroutine(coroutine);

        public void StopCoroutine(Coroutine coroutine) => 
            _coroutineRuner.StopCoroutine(coroutine);
    }
}
