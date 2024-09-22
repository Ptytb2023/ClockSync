using Exstensions;
using Infrastructure.Services.Coroutines;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Networking;

namespace Infrastructure.Services.Web
{
    public class WebService : IWebServiceAsync
    {
        public async Task<string> Request(string http)
        {
            UnityWebRequest request = UnityWebRequest.Get(http);

            await request;

            if (request.result == UnityWebRequest.Result.Success)
                return request.downloadHandler.text;
            else
                return null;
        }
    }
}