using App.ECS;

namespace App.Code.Services
{
    public interface ITimeService : IService
    {
        public float DeltaTime { get; }
    }
}