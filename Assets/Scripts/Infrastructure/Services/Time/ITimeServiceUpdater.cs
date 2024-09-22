using System;

namespace Infrastructure.Services.Times
{
    public interface ITimeServiceUpdater : ITimeService
    {
        void StartTime();
        void StopTime();
        void SetTime(DateTime newTime);

    }
}