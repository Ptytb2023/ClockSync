using System;
using UnityEngine;
using DG.Tweening;
using Clocks;

public class AnalogClock : BaseClock
{
    [SerializeField] private HandData _hourHand;
    [SerializeField] private HandData _minuteHand;
    [SerializeField] private HandData _secondHand;

    private DateTime _lastTime;

    protected override void OnUpdateTime(DateTime currentTime)
    {
        if (_lastTime == currentTime)
            return;

        float hours = currentTime.Hour;
        float minutes = currentTime.Minute;
        float seconds = currentTime.Second;

        float hoursRotation = CalculateHourRotation(hours, minutes);
        float minutesRotation = CalculateMinuteRotation(minutes);
        float secondsRotation = CalculateSecondRotation(seconds);

        UpdateHandRotation(_hourHand.Hand, hoursRotation, _hourHand.AnimationDuration);
        UpdateHandRotation(_minuteHand.Hand, minutesRotation, _minuteHand.AnimationDuration);
        UpdateHandRotation(_secondHand.Hand, secondsRotation, _secondHand.AnimationDuration);

        _lastTime = currentTime;
    }

    private float CalculateHourRotation(float hours, float minutes) =>
        (hours + minutes / TimeUnitConfiguration.MinutesPerHour) * TimeUnitConfiguration.DegreesPerHour;

    private float CalculateMinuteRotation(float minutes) =>
        minutes * TimeUnitConfiguration.DegreesPerMinute;

    private float CalculateSecondRotation(float seconds) =>
        seconds * TimeUnitConfiguration.DegreesPerSecond;

    private void UpdateHandRotation(Transform handTransform, float angle, float duration) =>
        handTransform.DOLocalRotate(new Vector3(0, 0, -angle), duration);
}
