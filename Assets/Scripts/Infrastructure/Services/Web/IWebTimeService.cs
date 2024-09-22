using System;

namespace Infrastructure.Services.Web
{
    public interface IWebTimeService : IService
    {
        void FetchTime(Action<DateTime> complete);
    }
}