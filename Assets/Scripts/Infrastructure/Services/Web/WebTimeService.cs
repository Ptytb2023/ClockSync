using Data;
using Infrastructure.Services.JSON;
using System;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Web
{
    public class WebTimeService : IWebTimeService
    {
        private const string URL = "https://yandex.com/time/sync.json";

        private readonly IJsonService _jsonService;
        private readonly IWebService _webServiceAsync;

        private Coroutine _coroutine;
        private Action<DateTime> _complete;

        [Inject]
        public WebTimeService(IWebService webServiceAsync, IJsonService jsonService)
        {
            _jsonService = jsonService;
            _webServiceAsync = webServiceAsync;
        }

        public void FetchTime(Action<DateTime> complete)
        {
            _webServiceAsync.Request(URL, OnCompliteRequest);

            _complete = complete;
        }

        private void OnCompliteRequest(string json)
        {
            TimeData data = _jsonService.Deserialize<TimeData>(json);

            var time = DateTimeOffset.FromUnixTimeMilliseconds(data.Time).UtcDateTime;

            _complete?.Invoke(time);
        }
    }
}
