using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static GameObject player;
    public static GameObject platform;

    private bool canTurn = false;
    private Vector3 startPosition;

    public static bool isDead = false;

    [SerializeField] private Rigidbody rb;

    public GameObject magic;
    public Transform magicStartPosition;
    private Rigidbody magicRb;

    private int livesLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        GenerateWorld.RunDummy();
        startPosition = this.transform.position; // get the start pos of the player

        magicRb = magic.GetComponent<Rigidbody>();
        isDead = false;

        livesLeft = PlayerPrefs.GetInt("Lives");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

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

            //If the rotations is less than 10 degress, so constrait Z pos
            if (transform.rotation.eulerAngles.y < 190 && transform.rotation.eulerAngles.y > 170
                 || transform.rotation.eulerAngles.y < 10 && transform.rotation.eulerAngles.y > -10)
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionZ;
            }

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

            //If the rotations is less than 10 degress, so constrait Z pos
            if (transform.rotation.eulerAngles.y < 190 && transform.rotation.eulerAngles.y > 170
                || transform.rotation.eulerAngles.y < 10 && transform.rotation.eulerAngles.y > -10)
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                magicRb.constraints = RigidbodyConstraints.FreezePositionZ;
            }

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
        if (collision.gameObject.tag.Equals("fire") || collision.gameObject.tag.Equals("wall"))
        {
            animator.SetTrigger("isDie");
            isDead = true;
            Invoke("RestartGame", 1f);
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

        if (other.gameObject.tag.Equals("fire"))
        {
            animator.SetTrigger("isDie");
            isDead = true;
            Invoke("RestartGame", 1f);
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
    
    // These lines are made for Spell

    public void CastSpell()
    {
        magic.transform.position = magicStartPosition.position;
        magic.SetActive(true);
        magicRb.AddForce(transform.forward * 4000);
        Invoke("KillMagic", 1);
    }

    public void KillMagic()
    {
        magic.SetActive(false);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
