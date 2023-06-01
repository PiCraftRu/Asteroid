using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid.Decorations
{
    public class Star : MonoBehaviour
    {
        private float _speed = 0;

        public void Start()
        {
            _speed = Random.Range(0.8f, 2f);
            transform.localScale = Vector3.one * Random.Range(0.3f, 1.2f);
            transform.position += Vector3.up * Random.Range(0.3f, 1.7f);
        }

        public void Update()
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
    }
}
