using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasJump : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Animator animator;
 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Jump()
    {
        animator.SetTrigger("IsJump");
        PlayerController.sfx[6].Play();
        rb.AddForce(Vector3.up * 200);
    }
}
