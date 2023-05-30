using UnityEngine;

namespace Asteroid.Animations
{
    public class SpaceObject : MonoBehaviour
    {
        [SerializeField] Material baseMaterial;
        private Vector3 speed = Vector3.zero;

        public void Start()
        {
            speed = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
            SetMaterial();
        }

        private void SetMaterial()
        {
            var material = new Material(baseMaterial);
            material.SetColor("_Color", new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            ));
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = material;
        }

        public void Update()
        {
            transform.Rotate(speed * Time.deltaTime * 6);
        }
    }
}
