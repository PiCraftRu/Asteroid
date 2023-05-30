using UnityEngine;

namespace Asteroid.Animations
{
    public class Appear : MonoBehaviour
    {
        public float size = 0;

        private float Interp(float x)
        {
            float c4 = (2 * Mathf.PI) / 3;

            return x == 0
              ? 0
              : x == 1
              ? 1
              : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10f - 0.75f) * c4) + 1;
        }

        private float InterpWithClamp(float x)
        {
            return Interp(Mathf.Clamp01(x));
        }

        private float time;

        public void Start()
        {
            time = Time.time;
            transform.localScale = Vector3.zero;
        }

        public void Update()
        {
            transform.localScale = InterpWithClamp(Time.time - time) * size * Vector3.one;
            if (Time.time - time > 1)
            {
                Destroy(this);
            }
        }
    }

}