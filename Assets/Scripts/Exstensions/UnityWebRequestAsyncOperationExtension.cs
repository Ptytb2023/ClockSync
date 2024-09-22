using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace Exstensions
{
    public static class UnityWebRequestAsyncOperationExtension
    {
        public static AsyncOperationAwaiter GetAwaiter(this UnityWebRequest request) =>
            new AsyncOperationAwaiter(request);
    }

    public struct AsyncOperationAwaiter : INotifyCompletion
    {
        private UnityWebRequestAsyncOperation _request;

        private Action _continuation;

        public AsyncOperationAwaiter(UnityWebRequest request) : this() =>
           _request = request.SendWebRequest();

        public bool IsCompleted => _request.isDone;

        public UnityWebRequestAsyncOperation GetResult()
        {
            if (_request.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                _request.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error request: {_request.webRequest.error}");
            }

            return _request;
        }

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            _request.completed += OnRequestCompleted;
        }

        private void OnRequestCompleted(AsyncOperation operation) => 
            _continuation.Invoke();
    }
}
