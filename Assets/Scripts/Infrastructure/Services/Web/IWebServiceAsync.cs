using System.Threading.Tasks;

namespace Infrastructure.Services.Web
{
    public interface IWebServiceAsync : IService
    {
        Task<string> Request(string url);
    }
}