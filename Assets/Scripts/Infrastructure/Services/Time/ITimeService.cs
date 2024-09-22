using System;

namespace Infrastructure.Services.Times
{
    public interface ITimeService : IService
    {
        event Action<DateTime> TimeUpdated;

        DateTime GetTime();
    }
}