using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Asteroid asteroid;


    void Start()
    {
        
    }

    void Update()
    {
        if (asteroid ==  null)
        {
            asteroid = FindFirstObjectByType<Asteroid>();
        }
        else
        {
            var gravitation = asteroid.GetGravitation();
            if (gravitation != null)
            {
                transform.position = Vector3.Lerp(transform.position, gravitation.transform.position + Vector3.up * 10 + Vector3.forward * 3, Time.deltaTime * 5);
            }
        }
    }
}
