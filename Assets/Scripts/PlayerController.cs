using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    private float _baseMovementSpeed;
    [SerializeField] private Transform _boneParent;
    [SerializeField] private Transform _spinePosition;
    public List<GameObject> goldList;
    public float movementSpeed = 5f;
    public float rotationSpeed = 15f;
    public float reduceSpeed;
    public int carry;
    public bool canMove;
    public int CarryLimit => goldList.Count;

    private void Start()
    {
        reduceSpeed = 0.5f;
        _baseMovementSpeed = movementSpeed;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Ragdoll(false);
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
            return;
        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

        _animator.SetBool("IsRunning", movementDirection != Vector3.zero);
        _animator.SetBool("IsCarrying", carry != 0);

        if (movementDirection == Vector3.zero)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        _rb.velocity = movementDirection * movementSpeed;

        var rotationDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);

    }

    public bool CollectgGold()
    {
        if (carry == CarryLimit)
            return false;

        goldList[carry].gameObject.SetActive(true);
        carry++;
        movementSpeed -= reduceSpeed;

        return true;
    }

    public int DropGold()
    {
        var carryingGold = carry;

        if (carryingGold == 0)
            return 0;

        foreach (var gold in goldList)
            gold.SetActive(false);

        carry = 0;
        movementSpeed = _baseMovementSpeed;

        return carryingGold;
    }

    public void Ragdoll(bool IsActive)
    {
        _animator.enabled = !IsActive;

        var colliders = _boneParent.GetComponentsInChildren<Collider>();
        var rigidbodies = _boneParent.GetComponentsInChildren<Rigidbody>();

        foreach (var collider in colliders)
            collider.enabled = IsActive;

        foreach (var rig in rigidbodies)
            rig.isKinematic = !IsActive;

        GetComponent<Collider>().enabled = !IsActive;

        canMove = !IsActive;

        if (!IsActive)
            StartCoroutine(CloseRagdoll());
    }

    public IEnumerator CloseRagdoll()
    {
        yield return new WaitForSeconds(3f);
        Ragdoll(false);
        transform.position = new Vector3(_spinePosition.position.x, 0, _spinePosition.position.z);
    }
}
