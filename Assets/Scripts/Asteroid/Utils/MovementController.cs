using UnityEngine;

namespace Asteroid.Utils
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;

        public enum MovementType
        {
            Linear,
            Orbittal,
        }

        [SerializeField] private MovementType _movementType = MovementType.Linear;

        [SerializeField] private Vector3 _center = Vector3.zero;

        [SerializeField] private Vector3 _direction = Vector3.forward;

        private Rigidbody _rigidbody;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            switch (_movementType)
            {
                case MovementType.Linear:
                    MoveLinear();
                    break;
                case MovementType.Orbittal:
                    MoveOrbittal();
                    break;
            }
        }

        private void MoveLinear()
        {
            _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * Time.deltaTime);
        }

        private void MoveOrbittal()
        {
            var angularSpeed = _speed / _radius;
            var angle = GetAngle() + angularSpeed * Time.deltaTime;
            var position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius + _center;
            _direction = (position - _rigidbody.position).normalized;
            _rigidbody.MovePosition(position);
        }

        private float _radius = 0;

        public void SetMoveOrbital()
        {
            
            _movementType = MovementType.Orbittal;
            _speed = _speed * Mathf.Sign(GetOutCenterVector().x);
        }

        private float GetAngle()
        {
            var direction = GetOutCenterVector().normalized;
            return Mathf.Atan2(direction.z, direction.x);
        }

        private Vector3 GetOutCenterVector()
        {
            return (_rigidbody.position - _center);
        }

        public void SetMoveOrbital(GameObject center)
        {
            SetCenter(center.transform.position);
            SetMoveOrbital();
        }

        public void SetMoveLinear()
        {
            _movementType = MovementType.Linear;
            _speed = Mathf.Abs(_speed);
        }

        public MovementType movementType
        {
            get
            {
                return _movementType;
            }
        }

        public Vector3 center
        {
            get
            {
                return _center;
            }
        }

        public void SetCenter(Vector3 center)
        {
            _center = center;
            _radius = GetOutCenterVector().magnitude;
        }

        public float speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
    }
}
