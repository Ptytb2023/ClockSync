using System;
using Zenject;

namespace Infrastructure.Services.Times
{
    public class TimeService : ITickable, ITimeServiceUpdater
    {
        private const float UpdateInterval = 1f;

        private readonly ITickService _tickService;

        private DateTime _currentTime;
        private DateTime _lastReportedTime;

        public event Action<DateTime> TimeUpdated;

        private float _timeSinceLastUpdate = 0f;
        private bool _isTick;

        [Inject]
        public TimeService(ITickService tickService)
        {
            _tickService = tickService;
            Initialize();
        }

        private void Initialize()
        {
            _currentTime = DateTime.Now;
            _lastReportedTime = _currentTime;
        }

        public void SetTime(DateTime newTime)
        {
            _currentTime = newTime;
            _lastReportedTime = newTime;
            NotifyTimeUpdated();
        }

        public DateTime GetTime() =>
            _currentTime;

        public void Tick()
        {
            if (!_isTick)
                return;

            _timeSinceLastUpdate += _tickService.DeltaTime;

            if (_timeSinceLastUpdate >= UpdateInterval)
            {
                UpdateTime(_timeSinceLastUpdate);
                _timeSinceLastUpdate = 0f;
            }
        }

        private void UpdateTime(float deltaTime)
        {
            _currentTime = _currentTime.AddSeconds(deltaTime);

            if (HasSecondChanged())
                NotifyTimeUpdated();
        }

        private bool HasSecondChanged() =>
            _currentTime.Second != _lastReportedTime.Second;

        private void NotifyTimeUpdated()
        {
            TimeUpdated?.Invoke(_currentTime);
            _lastReportedTime = _currentTime;
        }

        public void StartTime() =>
            _isTick = true;

        public void StopTime() =>
            _isTick = false;
    }
}