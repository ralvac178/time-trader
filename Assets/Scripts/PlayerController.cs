using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static GameObject player;
    public static GameObject platform;

    private bool canTurn = false;
    private Vector3 startPosition;

    public static bool isDead = false;

    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        GenerateWorld.RunDummy();
        startPosition = this.transform.position; // get the start pos of the player
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJump", true);
            rb.AddForce(Vector3.up * 200);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetBool("IsMagic", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummy.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
            {
                GenerateWorld.RunDummy();
            }

            transform.position = new Vector3(startPosition.x, transform.position.y,
                                            startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummy.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
            {
                GenerateWorld.RunDummy();
            }

            transform.position = new Vector3(startPosition.x, transform.position.y,
                                            startPosition.z);
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
        if (collision.gameObject.tag.Equals("fire"))
        {
            animator.SetTrigger("isDie");
            isDead = true;
        }
        else
        {
            platform = collision.gameObject;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
        {
            GenerateWorld.RunDummy();
        }
        

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

        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
        {
            GenerateWorld.RunDummy();
        }
    }

}
