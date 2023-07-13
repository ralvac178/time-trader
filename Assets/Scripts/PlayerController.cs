using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static GameObject player;
    public static GameObject platform;

    private bool canTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        GenerateWorld.RunDummy();
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
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            transform.Rotate(Vector3.up * 90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            transform.Rotate(Vector3.up * -90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(0.5f, 0, 0);
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

    private void OnCollisionEnter(Collision collision)
    {
        platform = collision.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        GenerateWorld.RunDummy();

        if (other is SphereCollider)
        {
            canTurn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
        {
            canTurn = false;
        }
    }

}
