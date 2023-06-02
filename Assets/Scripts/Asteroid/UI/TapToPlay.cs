using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid.UI
{
    public class TapToPlay : MonoBehaviour
    {
        private UnityEngine.UI.Image _image;
        [SerializeField] private GameObject _asteroidPrefab;

        public void Start()
        {
            _image = GetComponent<UnityEngine.UI.Image>();
        }

        public void Update()
        {
            _image.rectTransform.localScale = Mathf.Lerp(0.8f, 1f, Mathf.Abs(Mathf.Sin(Time.time * 2))) * Vector3.one;
            _image.rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(-20, 20, Mathf.Pow(Mathf.Sin(1.1f * Time.time), 2)));
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(_asteroidPrefab, new Vector3(2, 0, -5), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
