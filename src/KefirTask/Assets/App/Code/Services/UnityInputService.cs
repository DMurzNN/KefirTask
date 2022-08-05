using UnityEngine;

namespace App.Code.Services
{
    public class UnityInputService : IInputService
    {
        public Vector2 MainAxis => 
            new(Horizontal, Vertical);
        
        public float Horizontal { get; private set; }

        public float Vertical { get; private set; }
        public bool ShootBullet { get; private set; }

        public void Update()
        {
            if (Input.GetKey(KeyCode.W))
                Vertical = 1.0f;
            else if (Input.GetKey(KeyCode.S))
                Vertical = -1.0f;
            else
                Vertical = 0.0f;
            
            if (Input.GetKey(KeyCode.D))
                Horizontal = 1.0f;
            else if (Input.GetKey(KeyCode.A))
                Horizontal = -1.0f;
            else
                Horizontal = 0.0f;

            ShootBullet = Input.GetMouseButtonDown(0);
        }
    }
}