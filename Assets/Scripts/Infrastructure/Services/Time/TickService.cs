using UnityEngine;

namespace Infrastructure.Services.Times
{
    public class TickService : ITickService
    {
        public float DeltaTime => Time.deltaTime;
    }
}