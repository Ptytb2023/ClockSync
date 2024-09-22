using System;

namespace Infrastructure.Services.Web
{
    public interface IWebService : IService
    {
        void Request(string url, Action<string> complite);
    }
}