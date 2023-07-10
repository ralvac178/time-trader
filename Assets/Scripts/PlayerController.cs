using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
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
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * 90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(0.1f, 0, 0);
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
