using System;
using TMPro;
using UnityEngine;

namespace Clocks
{
    public class DigitalClock : BaseClock
    {
        [SerializeField] private TMP_InputField _outputText;

        protected override void OnUpdateTime(DateTime currentTime) => 
            _outputText.text = currentTime.ToString("HH:mm:ss");
    }
}
