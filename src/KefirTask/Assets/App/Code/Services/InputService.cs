using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Code.Services
{
    public class InputService : MonoBehaviour,
        IInputService
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public bool ShootBullet { get; private set; }
        public bool ShootLaser { get; private set; }

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            var value = callbackContext.ReadValue<Vector2>();
            Horizontal = value.x;
            Vertical = value.y;
        }

        public void OnShoot(InputAction.CallbackContext callbackContext) =>
            ShootBullet = callbackContext.ReadValueAsButton();

        public void OnShootLaser(InputAction.CallbackContext callbackContext) =>
            ShootLaser = callbackContext.ReadValueAsButton();

        private void LateUpdate()
        {
            ShootBullet = false;
            ShootLaser = false;
        }
    }
}