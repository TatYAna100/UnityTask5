using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Idle");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);            
            _animator.SetTrigger("Run");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.ResetTrigger("Run");
            _animator.SetTrigger("Idle");
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            _animator.SetTrigger("Run");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.ResetTrigger("Run");
            _animator.SetTrigger("Idle");
        }
    }
}