using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.Coroutines
{
    public interface ICoroutineService : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}
