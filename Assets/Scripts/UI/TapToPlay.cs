using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;
    [SerializeField] private GameObject _asteroidPrefab;

    public void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void Update()
    {
        _text.fontSize = Mathf.Lerp(25, 30, Mathf.Abs(Mathf.Sin(Time.time * 2)));
        _text.rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(-20, 20, Mathf.Pow(Mathf.Sin(1.1f * Time.time), 2)));
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_asteroidPrefab, new Vector3(2, 0, -5), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
