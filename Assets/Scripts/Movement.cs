using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private const string _idle = "Idle";
    private const string _run = "Run";
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(_idle);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);            
            _animator.SetTrigger(_run);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.ResetTrigger(_run);
            _animator.SetTrigger(_idle);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            _animator.SetTrigger(_run);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.ResetTrigger(_run);
            _animator.SetTrigger(_idle);
        }
    }
}
