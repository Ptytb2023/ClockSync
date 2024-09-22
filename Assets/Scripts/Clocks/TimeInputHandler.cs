using Infrastructure.Services.Times;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clocks
{
    public class ManualTimeSetting : MonoBehaviour
    {
        private const float FullCircleDegrees = 360f;
        private const int MaxHoursInAnalog = 12;

        [SerializeField] private DigitalClockUpdater _digitalClockUpdater;
        [SerializeField] private ClockHand _hourHand;
        [SerializeField] private ClockHand _minuteHand;

        [SerializeField] private Button _editButton;

        private bool _isStopTime;
        private ITimeServiceUpdater _timeService;

        [Inject]
        public void Construct(ITimeServiceUpdater timeServiceUpdater) =>
            _timeService = timeServiceUpdater;

        private void OnEnable()
        {
            SubscribeToEvents();
            _timeService.StopTime();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
            _timeService.StartTime();
        }

        private void SubscribeToEvents()
        {
            _editButton.onClick.AddListener(OnApplyButtonClicked);
            _editButton.interactable = true;
            _digitalClockUpdater.enabled = true;

            _hourHand.UptadeRotation += OnClockHandRotationChanged;
            _minuteHand.UptadeRotation += OnClockHandRotationChanged;
            _digitalClockUpdater.UpdateTime += UpdateCurrentTime;
        }

        private void UnsubscribeFromEvents()
        {
            _editButton.onClick.RemoveListener(OnApplyButtonClicked);
            _editButton.interactable = false;
            _digitalClockUpdater.enabled = false;

            _hourHand.UptadeRotation -= OnClockHandRotationChanged;
            _minuteHand.UptadeRotation -= OnClockHandRotationChanged;
            _digitalClockUpdater.UpdateTime -= UpdateCurrentTime;
        }

        private void OnApplyButtonClicked()
        {
            UpdateCurrentTime();
            gameObject.SetActive(false);
        }

        private void UpdateCurrentTime()
        {
            DateTime currentTime = _timeService.GetTime();

            TimeSpan newTimeSpan = new TimeSpan(
                _digitalClockUpdater.Hour,
                _digitalClockUpdater.Minute,
                _digitalClockUpdater.Second
            );

            DateTime newTime = currentTime.Date + newTimeSpan;

            _timeService.SetTime(newTime);
        }

        private void OnClockHandRotationChanged()
        {
            float hourRotation = GetNormalizedRotation(_hourHand.transform.localEulerAngles.z);
            float minuteRotation = GetNormalizedRotation(_minuteHand.transform.localEulerAngles.z);

            int minute = CalculateMinute(minuteRotation);
            int hour = CalculateHour(hourRotation, minuteRotation);

            UpdateDigitalClock(hour, minute);
        }

        private int CalculateMinute(float minuteRotation) =>
            Mathf.FloorToInt(minuteRotation / TimeUnitConfiguration.DegreesPerMinute) % TimeUnitConfiguration.MinutesPerHour;

        private int CalculateHour(float hourRotation, float minuteRotation)
        {
            int hour = Mathf.FloorToInt((hourRotation + (minuteRotation / TimeUnitConfiguration.DegreesPerHour)) / TimeUnitConfiguration.DegreesPerHour) % TimeUnitConfiguration.HoursPerDay;
            return (hour == 0) ? MaxHoursInAnalog : hour;
        }

        private void UpdateDigitalClock(int hour, int minute)
        {
            DateTime currentTime = _timeService.GetTime();
            DateTime newTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hour, minute, 0);
            _digitalClockUpdater.SetTime(newTime.ToString("HH:mm:ss"));
        }

        private float GetNormalizedRotation(float rotation) =>
            (FullCircleDegrees - rotation) % FullCircleDegrees;
    }
}
