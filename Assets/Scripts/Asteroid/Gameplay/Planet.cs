using UnityEngine;

namespace Asteroid.Gameplay
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private float _radius = 1;

        public static Planet GetNearestPlanet(GameObject gameObject, out float distance)
        {
            var planets = FindObjectsByType<Planet>(FindObjectsSortMode.None);
            distance = Mathf.Infinity;
            Planet planet = null;
            foreach (var pl in planets)
            {
                var possibleDistance = Vector3.Distance(gameObject.transform.position, pl.transform.position);
                if (possibleDistance < distance) {
                    planet = pl;
                    distance = possibleDistance;
                }
            }
            return planet;
        }

        public static Planet GetNearestPlanetWithRadius(GameObject gameObject, out float distance)
        {
            var planet = GetNearestPlanet(gameObject, out distance);
            if (planet != null)
            {
                if (distance > planet._radius)
                {
                    return null;
                }
            }
            return planet;
        }
    }
}

