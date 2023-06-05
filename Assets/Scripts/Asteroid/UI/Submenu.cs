using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submenu : MonoBehaviour
{
    private Animator _animator;
    private float _time = -1f;
    private float _delay = 1;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Close(float delay)
    {
        _time = Time.time;
        _delay = delay;
        _animator.SetBool("close", true);
    }

    public void Update()
    {
        if (_time > 0 && Time.time - _time > _delay)
        {
            Destroy(gameObject);
        }
    }
}
