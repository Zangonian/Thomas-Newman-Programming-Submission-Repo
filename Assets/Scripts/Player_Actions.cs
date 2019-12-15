using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Actions : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private float speed = 10f;
    private bool dead = false;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private int jumpCount;
    private int jumpCountLimit;
    private int coinsCollected = 0;
    private int coinValue = 1;
    public GameObject gameManager;
    
    public Animator animator;
    

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        dead = false;
        jumpCountLimit = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;

        Movement();
        
        if(Grounded())
        {
            animator.SetBool("isJumping", false);
            jumpCount = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Grounded())
            {
                Jump();
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if (jumpCount < jumpCountLimit)
                    {
                        Jump();
                        jumpCount++;
                    }
                }
            }
        }
        if (Input.GetMouseButton (0))
        {
            if (Grounded())
            {
                Jump();
            }
            else
            {
                if (Input.GetMouseButtonDown (0))
                {
                    if (jumpCount < jumpCountLimit)
                    {
                        Jump();
                        jumpCount++;
                    }
                }
            }
        }
    }

    private bool Grounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private void Movement()
    {
        rigidbody2d.velocity = new Vector2(+speed, rigidbody2d.velocity.y);
    }

    private void Jump()
    {
        float jumpVelocity = 20f;
        rigidbody2d.velocity = Vector2.up * jumpVelocity;
        animator.SetBool("isJumping", true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Coin"))
        {
            Destroy(coll.gameObject);
            coinsCollected++;
            Score_Tracking.instance.ChangeCoinCount(coinValue);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Death();
        }      
    }

    private void Death()
    {
        dead = true;
        SceneManager.LoadScene("LoseScene");
    }
}
