using System;
using Infrastructure.Services.Times;
using Infrastructure.Services.Web;
using UnityEngine;
using Zenject;

namespace TimeSynchronizer
{
    public class WebTimeSynchronizer : IDisposable, IWebTimeSynchronizer
    {
        private const int TimeUpdateIntervalHours = 1;

        private readonly ITimeServiceUpdater _timeService;
        private readonly IWebTimeService _webService;

        private Coroutine _updateTimeCorotine;

        private int _lastUpdatedHour;
        private bool _isUpdating;

        [Inject]
        public WebTimeSynchronizer(ITimeServiceUpdater timeService, IWebTimeService webService )
        {
            _timeService = timeService;
            _webService = webService;
        }

        public void SetActive(bool active)
        {
            if (active)
                Activate();
            else
                Deactivate();
        }

        private void Activate()
        {
            _timeService.TimeUpdated += OnTimeUpdate;
            _lastUpdatedHour = _timeService.GetTime().Hour - 1;

            UpdateTime(_timeService.GetTime());
        }

        private void Deactivate()
        {
            _timeService.TimeUpdated -= OnTimeUpdate;
            _isUpdating = false;
        }

        private void OnTimeUpdate(DateTime currentTime)
        {
            if (!_isUpdating && ShouldUpdateTime(currentTime))
            {
                _isUpdating = true;

                _webService.FetchTime(UpdateTime);
            }
        }

        private bool ShouldUpdateTime(DateTime currentTime) =>
            currentTime.Hour >= _lastUpdatedHour + TimeUpdateIntervalHours;

        private void UpdateTime(DateTime dateTime)
        {
            _timeService.SetTime(dateTime);
            _lastUpdatedHour = dateTime.Hour;

            _isUpdating = false;
        }

        public void Dispose() =>
            _timeService.TimeUpdated -= OnTimeUpdate;
    }
}
