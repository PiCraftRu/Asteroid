using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public float radius;
    [SerializeField] private GameObject prefabBadAsteroid;
    [SerializeField] private float maxRadiusSpawnBadAsteroid;

    public class Child
    {
        public float distance;
        public float speed;
        public GameObject gameObject;

        public Vector3 direction;
    }

    private List<Child> children = new List<Child>();

    public void Start()
    {
        if (prefabBadAsteroid != null)
        {
            var count = Random.Range(0, 2);
            for (int i = 0; i < count; i++)
            {
                var position = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)).normalized * Random.Range(1.5f, maxRadiusSpawnBadAsteroid) + transform.position;
                var gameObject = Instantiate(prefabBadAsteroid, position, Quaternion.identity);
                gameObject.AddComponent<AppearAnimator>().size = 1;
                Add(gameObject, Random.Range(0.5f, 2f));
            }
        }
    }

    public void Update()
    {
        foreach (var item in children)
        {
            Move(item);
        }
    }

    public void Add(GameObject item, float speed)
    {
        var direction = (item.transform.position - transform.position).normalized;
        var child = new Child();
        child.distance = Vector3.Distance(transform.position, item.transform.position);
        child.speed = speed * (direction.x > 0 ? 1 : -1);
        child.gameObject = item;
        children.Add(child);
    }

    public void Remove(GameObject item)
    {
        children.RemoveAll(c => c.gameObject == item);
    }

    public Vector3 GetDirection(GameObject item)
    {
        foreach (var child in children)
        {
            if (child.gameObject == item)
            {
                return child.direction;
            }
        }
        return Vector3.zero;
    }

    private void Move(Child child)
    {
        var direction = (child.gameObject.transform.position - transform.position).normalized;
        var angularSpeed = child.speed / child.distance;
        var angle = Mathf.Atan2(direction.z, direction.x) + angularSpeed * Time.deltaTime;
        var position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * child.distance + transform.position;
        child.direction = (position - child.gameObject.transform.position).normalized;
        child.gameObject.transform.position = position;
    }

    public static Gravitation Find(GameObject item, out float distance)
    {
        foreach (var gr in FindObjectsByType<Gravitation>(FindObjectsSortMode.None))
        {
            var d = Vector3.Distance(item.transform.position, gr.transform.position);
            if (d <= gr.radius)
            {
                distance = d;
                return gr;
            }
        }
        distance = -1;
        return null;
    }
}
