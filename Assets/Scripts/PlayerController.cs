using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static GameObject player;
    public static GameObject platform;

    public static bool canTurn = false;
    public static Vector3 startPosition;

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

    private InputMaster inputMaster;

    [SerializeField] private HasTurn hasTurn;
    [SerializeField] private HasJump hasJump;
    [SerializeField] private HasShoot hasShoot;
    [SerializeField] private HasMoveHorizontal hasMoveHorizontally;

    private void Awake()
    {
        inputMaster = new InputMaster();

        if (sfx != null)
        {
            sfx[4].mute = false;
            sfx[5].mute = false;
        }

        canFall = false;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
        inputMaster.Player.Shoot.performed += _ => hasShoot.Shoot();
        inputMaster.Player.Jump.performed += _ => hasJump.Jump();

        inputMaster.Player.Horizontal.performed += context =>
        {
            hasMoveHorizontally.MoveHorizontal(0.5f * context.ReadValue<Vector2>());
        };
        
        inputMaster.Player.TurnRight.performed += _ => hasTurn.TurnRight();
        inputMaster.Player.TurnLeft.performed += _ => hasTurn.TurnLeft();
    }

    private void OnDisable()
    {
        inputMaster.Player.Shoot.performed -= _ => hasShoot.Shoot();
        inputMaster.Player.Jump.performed -= _ => hasJump.Jump();
        inputMaster.Player.Horizontal.performed -= context =>
        {
            hasMoveHorizontally.MoveHorizontal(0.5f * context.ReadValue<Vector2>());
        };
        inputMaster.Player.TurnRight.performed -= _ => hasTurn.TurnRight();
        inputMaster.Player.TurnLeft.performed -= _ => hasTurn.TurnLeft();
        inputMaster.Disable();
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
