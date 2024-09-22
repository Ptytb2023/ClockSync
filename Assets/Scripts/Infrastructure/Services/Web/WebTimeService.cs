using Data;
using Infrastructure.Services.JSON;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Infrastructure.Services.Web
{
    public class WebTimeService : IWebTimeService
    {
        private const string URL = "https://yandex.com/time/sync.json";

        private readonly IJsonService _jsonService;
        private readonly IWebServiceAsync _webServiceAsync;

        [Inject]
        public WebTimeService(IWebServiceAsync webServiceAsync, IJsonService jsonService)
        {
            _jsonService = jsonService;
            _webServiceAsync = webServiceAsync;
        }

        public async Task<DateTime> FetchTimeAsync()
        {
            var json = await _webServiceAsync.Request(URL);

            TimeData data = _jsonService.Deserialize<TimeData>(json);

            return DateTimeOffset.FromUnixTimeMilliseconds(data.Time).UtcDateTime;
        }
    }
}
