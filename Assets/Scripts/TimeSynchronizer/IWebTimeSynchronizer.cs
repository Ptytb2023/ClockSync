using Infrastructure.Services;

namespace TimeSynchronizer
{
    public interface IWebTimeSynchronizer : IService
    {
        void SetActive(bool active);
    }
}