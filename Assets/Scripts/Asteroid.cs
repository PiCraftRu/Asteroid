using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed = 1.0f;
    private Vector3 _direction = Vector3.forward;
    private List<Gravitation> _memory = new List<Gravitation>();
    private Collider _collider;

    private enum Movement
    {
        Free,
        Gravitation,
    }

    private Movement _movement = Movement.Free;

    [SerializeField] private GameObject planetPrefab;

    public void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public void Update()
    {
        if (_movement == Movement.Free)
        {
            FreeMove();
        }
        if (Input.GetMouseButtonDown(0))
        {
            MakeFree();
        }
        CheckToDie();
    }

    private float _distanceToPlanet = Mathf.Infinity;
    private Gravitation _gravitation = null;
    
    private void CheckToDie()
    {
        var point = Camera.main.WorldToViewportPoint(transform.position);
        if (!CheckCollision() && (point.x < 1.1f && point.x >= -0.1f) )
        {
            return;
        }
        gameObject.AddComponent<Explosion>();
        Destroy(this);
    }

    public Gravitation GetGravitation()
    {
        return _gravitation;
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

    public void FreeMove()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        _speed += 1.01f * Time.deltaTime;
        float distance = 0;
        var gravitation = Gravitation.Find(gameObject, out distance);
        if (gravitation != null && this._gravitation != gravitation && _memory.FindIndex(c => c == gravitation) == -1)
        {
            if (distance < _distanceToPlanet)
            {
                _distanceToPlanet = distance;
            }
            else
            {
                _gravitation = gravitation;
                _movement = Movement.Gravitation;
                gravitation.Add(gameObject, _speed);
                SpawnPlanet(gravitation);
                _memory.Add(gravitation);
            }
        }
    }

    private void MakeFree()
    {
        if (_gravitation != null && _gravitation.GetDirection(gameObject).magnitude > 0.5f)
        {
            _distanceToPlanet = Mathf.Infinity;
            _movement = Movement.Free;
            _direction = _gravitation.GetDirection(gameObject);
            _gravitation.Remove(gameObject);
        }
    }

    private void SpawnPlanet(Gravitation relative)
    {
        var gameObject = Instantiate(planetPrefab, new Vector3(Random.Range(-1f, 1f) + relative.transform.position.x, 0, relative.transform.position.z + 6), Quaternion.identity);
        gameObject.AddComponent<AppearAnimator>().size = 50;
    }
}
