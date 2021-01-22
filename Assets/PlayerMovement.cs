using UnityEngine;
namespace DefaultNamespace
{
    public class PlayerMovement : Movement
    {
        [SerializeField]
        private Transform playerCamera = null;
        
        protected override void Move()
        {
            var targetAngle = Mathf.Atan2(_inputManager._directionInput.x, _inputManager._directionInput.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            var smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized * (_currentMovementSpeed * Time.deltaTime));
        }
    }
}