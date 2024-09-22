using Newtonsoft.Json;

namespace Infrastructure.Services.JSON
{
    public class JsonNetService : IJsonService
    {
        public TModel Deserialize<TModel>(string json) =>
            JsonConvert.DeserializeObject<TModel>(json);

        public string Serialize<TModel>(TModel model) =>
            JsonConvert.SerializeObject(model, Formatting.Indented);

    }
}