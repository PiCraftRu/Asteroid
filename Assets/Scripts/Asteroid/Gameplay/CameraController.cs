using Asteroid.Utils;
using UnityEngine;

namespace Asteroid.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        private Asteroid _asteroid;
        private Vector3 _position;
        private Rigidbody _rigidbody;

        void Start()
        {
            _position = transform.position;
            if ( Application.isMobilePlatform )
            {
                Application.targetFrameRate = 60;
            }
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (_asteroid == null)
            {
                _asteroid = FindFirstObjectByType<Asteroid>();
            }
            else
            {
                var controller = _asteroid.GetComponent<MovementController>();
                if (controller.movementType == MovementController.MovementType.Orbittal)
                {
                    _position = controller.center + Vector3.up * 10 + Vector3.forward * 3;
                }
            }
            _rigidbody.MovePosition(Vector3.Lerp(_rigidbody.position, _position, Time.deltaTime * 5));
        }
    }
}
