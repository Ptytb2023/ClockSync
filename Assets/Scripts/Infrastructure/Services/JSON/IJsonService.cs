namespace Infrastructure.Services.JSON
{
    public interface IJsonService : IService
    {
        TModel Deserialize<TModel>(string json);
        string Serialize<TModel>(TModel model);
    }
}