using Infrastructure.Services.Times;
using TimeSynchronizer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clocks
{
    public class AutoUpdaterTime : MonoBehaviour
    {
        [SerializeField] private ManualTimeSetting _timeInputController;

        private IWebTimeSynchronizer _webTimeSynchronizer;
        private ITimeServiceUpdater _timeServiceUpdater;

        [Inject]
        public void Construct(IWebTimeSynchronizer webTimeSynchronizer, ITimeServiceUpdater timeServiceUpdater)
        {
            _timeServiceUpdater = timeServiceUpdater;
            _webTimeSynchronizer = webTimeSynchronizer;
        }

        private void Start()
        {
            _timeServiceUpdater.StartTime();
            _webTimeSynchronizer.SetActive(true);

            _timeInputController.gameObject.SetActive(false);
        }

        public void OnToggleChanged(bool toggle)
        {
            bool active = !toggle;

            _timeInputController.gameObject.SetActive(active);
            _webTimeSynchronizer.SetActive(toggle);
        }
    }
}