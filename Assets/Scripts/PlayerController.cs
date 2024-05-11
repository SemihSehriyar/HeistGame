using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    public float movementSpeed = 5f;
    public float rotationSpeed = 15f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3 (horizontal, 0, vertical);

        _animator.SetBool("IsRunning",movementDirection != Vector3.zero);

        if(movementDirection == Vector3.zero )
        {
            return;
        }

        _rb.velocity = movementDirection * movementSpeed;

        var rotationDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);
    }
}
