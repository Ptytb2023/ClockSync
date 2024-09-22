using Infrastructure.Services.Coroutines;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Infrastructure.Services.Web
{
    public class WebService : IWebService
    {
        private ICoroutineService _coroutineService;

        [Inject]
        public WebService(ICoroutineService coroutineService) => 
            _coroutineService = coroutineService;

        public void Request(string url, Action<string> complite) =>
            _coroutineService.StartCoroutine(RequestCoroutine(url, complite));

        private IEnumerator RequestCoroutine(string url, Action<string> onComplete)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                onComplete?.Invoke(request.downloadHandler.text);
            else
            {
                onComplete?.Invoke(null);
                Debug.LogError($"Error: {request.error}");
            }
        }
    }
}