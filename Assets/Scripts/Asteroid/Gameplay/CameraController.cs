using Asteroid.Utils;
using UnityEngine;

namespace Asteroid.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        private Asteroid _asteroid;
        private Vector3 _position;

        void Start()
        {
            _position = transform.position;
        }

        void Update()
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
            transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime * 5);

        }
    }
}
