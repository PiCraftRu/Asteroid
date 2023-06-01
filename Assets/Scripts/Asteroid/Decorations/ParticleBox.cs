using UnityEngine;

namespace Asteroid.Decorations
{
    public class ParticleBox : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private int _count;

        private GameObject[] _particles; 

        public void Start()
        {
            _particles = new GameObject[_count];
            for (int i = 0; i < _count; i++)
            {
                _particles[i] = Spawn();
            }
        }

        private GameObject Spawn()
        {
            return Instantiate(_particlePrefab, new Vector3(Random.Range(-_size.x / 2, _size.x / 2), 0, Random.Range(-_size.y / 2, _size.y / 2)) + transform.position, Quaternion.identity);
        }

        public void Update()
        {
            SlideParticles();
        }

        private void SlideParticles()
        {
            foreach (var item in _particles)
            {
                var position = item.transform.position - transform.position;
                if (position.x <= -_size.x / 2)
                {
                    item.transform.position = item.transform.position + _size.x * Vector3.right;
                }
                if (position.z <= -_size.y / 2)
                {
                    item.transform.position = item.transform.position + _size.y * Vector3.forward;
                }
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(transform.position, new Vector3(_size.x, 1, _size.y));
        }
    }
}
