using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.Web
{
    public interface IWebTimeService : IService
    {
        Task<DateTime> FetchTimeAsync();
    }
}