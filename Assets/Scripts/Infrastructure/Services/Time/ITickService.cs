namespace Infrastructure.Services.Times
{
    public interface ITickService : IService
    {
        float DeltaTime { get; }
    }
}