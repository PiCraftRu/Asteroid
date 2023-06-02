using Asteroid.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMProPlaceholder : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;


    void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _text.text = _text.text.Replace("%asteroid_score%", FindFirstObjectByType<Asteroid.Gameplay.Asteroid>().passedPlanets.ToString());
        _text.text = _text.text.Replace("%asteroid_speed%", FindFirstObjectByType<Asteroid.Gameplay.Asteroid>().speed.ToString("0.00"));
    }
}
