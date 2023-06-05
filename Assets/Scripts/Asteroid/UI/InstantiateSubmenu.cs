using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSubmenu : MonoBehaviour
{
    [SerializeField] GameObject _submenuPrefab;

    public void Spawn()
    {
        Instantiate(_submenuPrefab, FindFirstObjectByType<Canvas>().transform);
    }
}
