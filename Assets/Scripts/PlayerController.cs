using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public float movementSpeed = 5f;
    public float rotationSpeed = 15f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3 (horizontal, 0, vertical);

        if(movementDirection == Vector3.zero )
        {
            Debug.Log("DUUUUUR");
            return;
        }

        _rb.velocity = movementDirection * movementSpeed;

        var rotationDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);
    }
}
