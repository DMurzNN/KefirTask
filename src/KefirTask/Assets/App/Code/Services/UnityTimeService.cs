using UnityEngine;

namespace App.Code.Services
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime { get; private set; }
        public float PrevDeltaTime { get; private set; }

        public void Update()
        {
            PrevDeltaTime = DeltaTime;
            DeltaTime = Time.deltaTime;
        }
    }
}