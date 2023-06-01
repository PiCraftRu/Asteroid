using Asteroid.Animations;
using Asteroid.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Asteroid.Gameplay
{
    public class Asteroid : MonoBehaviour
    {
        private MovementController _controller;

        private Collider _collider;

        [SerializeField] private GameObject _prefabPlanet;

        [SerializeField] private GameObject _prefabDeathScreen;

        public void Start()
        {
            _controller = GetComponent<MovementController>();
            _collider = GetComponent<Collider>();
        }

        private List<Planet> _passedPlanets = new List<Planet>();

        private float _distanceToPlanet = Mathf.Infinity;

        public void Update()
        {
            Calculate();
            Click();
            CheckToDie();
            IncreaseSpeed();
        }

        private void IncreaseSpeed()
        {
            if (_controller.movementType == MovementController.MovementType.Linear)
            {
                _controller.speed += Time.deltaTime * 0.7f;
            }
        }

        private void Calculate()
        {
            var distance = 0f;
            var planet = Planet.GetNearestPlanetWithRadius(gameObject, out distance);
            if (planet != null && !_passedPlanets.Contains(planet))
            {
                if (distance < _distanceToPlanet)
                {
                    _distanceToPlanet = distance;
                }
                else
                {
                    _controller.SetMoveOrbital(planet.gameObject);
                    _passedPlanets.Add(planet);
                    SpawnPlanet();
                    DestroyUnusedPlanets();
                }
            }
        }

        public void DestroyUnusedPlanets()
        {
            if (_passedPlanets.Count > 3)
            {
                _passedPlanets.RemoveAt(0);
                Destroy(_passedPlanets[0].gameObject);
            }
        }

        private void Click()
        {
            if (Input.GetMouseButtonDown(0) && _controller.movementType == MovementController.MovementType.Orbittal)
            {
                _controller.SetMoveLinear();
                _distanceToPlanet = Mathf.Infinity;
            }
        }

        private void CheckToDie()
        {
            var point = Camera.main.WorldToViewportPoint(transform.position);
            if (!CheckCollision() && (point.x < 1.1f && point.x >= -0.1f))
            {
                return;
            }
            gameObject.AddComponent<Explosion>();
            Destroy(_controller);
            Destroy(this);
            Instantiate(_prefabDeathScreen, FindFirstObjectByType<Canvas>().transform);
        }

        private bool CheckCollision()
        {
            var overlapped = Physics.OverlapSphere(transform.position, 1);
            foreach (var other in overlapped)
            {
                if (other == _collider)
                {
                    continue;
                }
                Vector3 direction;
                float distance;
                var collided = Physics.ComputePenetration(_collider, transform.position, transform.rotation, other, other.transform.position, other.transform.rotation, out direction, out distance);
                if (collided)
                {
                    return true;
                }
            }
            return false;
        }

        private void SpawnPlanet()
        {
            var gm = Instantiate(_prefabPlanet, _controller.center + Vector3.forward * 6 + Vector3.right * Random.Range(-1f, 1f), Quaternion.identity);
            gm.AddComponent<Appear>().size = 50;
        }
    }
}
