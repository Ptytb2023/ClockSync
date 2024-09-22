using Clocks;
using System;
using TMPro;
using UnityEngine;

public class DigitalClockUpdater : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputTime;

    public event Action UpdateTime;

    public int Second { get; private set; }
    public int Minute { get; private set; }
    public int Hour { get; private set; }

    private void OnEnable()
    {
        _inputTime.interactable = true;
        _inputTime.onValueChanged.AddListener(OnTimeInputChanged);
        _inputTime.onEndEdit.AddListener(OnTimeInputCompleted); 
    }

    private void OnDisable()
    {
        _inputTime.interactable = false;
        _inputTime.onValueChanged.RemoveListener(OnTimeInputChanged);
        _inputTime.onEndEdit.RemoveListener(OnTimeInputCompleted); 
    }

    public void SetTime(string time)
    {
        _inputTime.text = time;
    }

    private void OnTimeInputChanged(string input)
    {
        string[] timeParts = input.Trim().Split(':');

        int hour = 0, minute = 0, second = 0;

        if (timeParts.Length > 0 && int.TryParse(timeParts[0], out hour))
            Hour = hour % TimeUnitConfiguration.HoursPerDay;

        if (timeParts.Length > 1 && int.TryParse(timeParts[1], out minute))
            Minute = minute % TimeUnitConfiguration.MinutesPerHour;

        if (timeParts.Length > 2 && int.TryParse(timeParts[2], out second))
            Second = second % TimeUnitConfiguration.SecondsPerMinute;

        _inputTime.text = $"{Hour}:{Minute}:{Second}";
    }

    private void OnTimeInputCompleted(string input)
    {
        string formattedTime = $"{Hour:D2}:{Minute:D2}:{Second:D2}";

        _inputTime.text = formattedTime;

        UpdateTime?.Invoke();
    }
}
