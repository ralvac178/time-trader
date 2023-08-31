using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasShoot : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void Shoot()
    {
        animator.SetBool("IsMagic", true);
    }
}
