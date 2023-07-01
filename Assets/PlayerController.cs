using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJump", true);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetBool("IsMagic", true);
        }
    }

    public void StopJump()
    {
        animator.SetBool("IsJump", false);
    }

    public void StopMagic()
    {
        animator.SetBool("IsMagic", false);
    }
}
