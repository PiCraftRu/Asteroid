using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trail : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float _time = 0;
    private Vector3[] _positions;
    private readonly float delay = 0.01f;

    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        _positions = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, transform.position);
            _positions[i] = transform.position;
        }
        _time = Time.time;

    }

    public void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        if (Time.time - _time >= delay)
        {
            Step();
            _time = Time.time;
        }
        Interpolate();
    }

    private void Step()
    {
        for (int i = _positions.Length - 1; i >= 1; i--)
        {
            _positions[i] = lineRenderer.GetPosition(i - 1);
        }
    }

    private void Interpolate()
    {
        for (int i = 1; i < _positions.Length; i++)
        {
            lineRenderer.SetPosition(i, Vector3.Lerp(lineRenderer.GetPosition(i), _positions[i], Time.deltaTime * 15));
        }
    }
}
