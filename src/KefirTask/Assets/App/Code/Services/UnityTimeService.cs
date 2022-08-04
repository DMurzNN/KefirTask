using UnityEngine;

namespace App.Code.Services
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime { get; private set; }

        public void Update() => 
            DeltaTime = Time.deltaTime;
    }
}