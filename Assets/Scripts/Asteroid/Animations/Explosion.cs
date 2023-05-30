using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroid.Animations
{
    public class Explosion : MonoBehaviour
    {
        private float Interp(float x)
        {
            return 1 - Mathf.Pow(1 - x, 5);
        }

        private float InterpWithClamp(float x)
        {
            return Interp(Mathf.Clamp01(x));
        }

        private float time;

        public void Start()
        {
            time = Time.time;
        }

        public void Update()
        {
            transform.localScale = (1 - InterpWithClamp(Time.time - time)) * Vector3.one;
            if (Time.time - time > 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Destroy(gameObject);
            }
        }
    }
}
