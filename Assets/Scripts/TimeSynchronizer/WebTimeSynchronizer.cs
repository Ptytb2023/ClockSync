using System;
using System.Threading.Tasks;
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

        private int _lastUpdatedHour;
        private bool _isUpdating;
        private bool _isActive;

        [Inject]
        public WebTimeSynchronizer(ITimeServiceUpdater timeService, IWebTimeService webService)
        {
            _timeService = timeService;
            _webService = webService;
        }

        public void SetActive(bool active)
        {
            if (_isActive == active)
                return;

            _isActive = active;

            if (active)
                Activate();
            else
                Deactivate();
        }

        private void Activate()
        {
            _timeService.TimeUpdated += OnTimeUpdate;
            _lastUpdatedHour = _timeService.GetTime().Hour;

            _ = UpdateTimeAsync();
        }

        private void Deactivate()
        {
            _timeService.TimeUpdated -= OnTimeUpdate;
            _isUpdating = false;
            _isActive = false;
        }

        private void OnTimeUpdate(DateTime currentTime)
        {
            if (!_isUpdating && ShouldUpdateTime(currentTime))
            {
                _ = UpdateTimeAsync();
            }
        }

        private bool ShouldUpdateTime(DateTime currentTime) =>
            currentTime.Hour >= _lastUpdatedHour + TimeUpdateIntervalHours;

        private async Task UpdateTimeAsync()
        {
            if (_isUpdating) 
                return; 

            _isUpdating = true;

            try
            {
                DateTime newTime = await _webService.FetchTimeAsync();
                _timeService.SetTime(newTime);
                _lastUpdatedHour = newTime.Hour;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                Deactivate(); 
            }
            finally
            {
                _isUpdating = false;
            }
        }

        public void Dispose() => 
            _timeService.TimeUpdated -= OnTimeUpdate;
    }
}
