using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] private Texture astroIcon, diedAstroIcon;

    [SerializeField] private RawImage[] icons;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI highScoreText;

    public static AudioSource[] sfx;

    public bool canFall = false;

    private Ray ray;
    private RaycastHit hit;

    public LayerMask layerMask;

    private void Awake()
    {
        if (sfx != null)
        {
            sfx[4].mute = false;
            sfx[5].mute = false;
        }

        canFall = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        GenerateWorld.RunDummy();
        startPosition = this.transform.position; // get the start pos of the player

        magicRb = magic.GetComponent<Rigidbody>();
        isDead = false;

        livesLeft = PlayerPrefs.GetInt("Lives");

        for (int i = 0; i < icons.Length; i++)
        {
            if (i >= livesLeft)
            {
                icons[i].texture = diedAstroIcon;
            }
        }

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScoreText.text = $"Highest Score: {PlayerPrefs.GetInt("highScore")}";
        }
        else
        {
            highScoreText.text = $"Highest Score: 0";
        }

        sfx = GameObject.Find("GameData").GetComponents<AudioSource>();
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
            animator.SetTrigger("IsJump");
            sfx[6].Play();
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

        if (canFall)
        {
            sfx[4].mute = true;
            sfx[5].mute = true;
            animator.SetTrigger("isFalling");
        }

        // This is a Raycast
        ray = new Ray(transform.position, Vector3.down);
        //if ()
        bool testRay = Physics.Raycast(ray, out hit, 8, layerMask, QueryTriggerInteraction.Ignore);
        bool testHightPos = transform.position.y < (platform.transform.position.y + 0.5f);
        if (testHightPos)
        {
            if (!testRay)
            {
                canFall = true;
            }

        }
        else
        {
            canFall = false;
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
        if ((collision.gameObject.tag.Equals("fire") || collision.gameObject.tag.Equals("wall")) && !isDead)
        {
            animator.SetTrigger("isDie");

            sfx[2].Play();
            isDead = true;
            livesLeft--;
            PlayerPrefs.SetInt("Lives", livesLeft);

            if (livesLeft > 0)
            {
                Invoke("RestartGame", 1f);
            }
            else
            {
                icons[0].texture = diedAstroIcon;
                gameOverPanel.SetActive(true);

                PlayerPrefs.SetInt("lastScore", PlayerPrefs.GetInt("Score"));
                if (PlayerPrefs.HasKey("highScore"))
                {
                    int hs = PlayerPrefs.GetInt("highScore");
                    if (hs < PlayerPrefs.GetInt("Score"))
                    {
                        PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("Score"));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("Score"));
                }
            }
            
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

        if (other.gameObject.tag.Equals("fire") && !isDead)
        {
            isDead = true;
            sfx[2].Play();
            livesLeft--;
            PlayerPrefs.SetInt("Lives", livesLeft);

            if (livesLeft > 0)
            {
                Invoke("RestartGame", 1f);
            }
            else
            {
                icons[0].texture = diedAstroIcon;
                gameOverPanel.SetActive(true);

                PlayerPrefs.SetInt("lastScore", PlayerPrefs.GetInt("Score"));
                if (PlayerPrefs.HasKey("highScore"))
                {
                    int hs = PlayerPrefs.GetInt("highScore");
                    if (hs < PlayerPrefs.GetInt("Score"))
                    {
                        PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("Score"));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("Score"));
                }
            }
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
        sfx[1].Play();
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

    void PlayStepFoot1()
    {
        sfx[4].Play();
    }

    void PlayStepFoot2()
    {
        sfx[5].Play();
    }
}
