using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private CapsuleCollider2D myCC;

    private string RUNNING_ANIMATION = "isRunning";
    private string CLIMBING_ANIMATION = "isClimbing";
    private string GROUND_LAYER = "Ground";
    private string CLIMBING_LAYER = "Climbing";

    private float gravityScaleAtStart;
    private bool isAlive = true;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCC = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }
    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) {return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        if (!myCC.IsTouchingLayers(LayerMask.GetMask(GROUND_LAYER))) {return;}
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool(RUNNING_ANIMATION, playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerhasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerhasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myCC.IsTouchingLayers(LayerMask.GetMask(CLIMBING_LAYER)))
        {
            
            rb.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool(CLIMBING_ANIMATION, false);
            return;
        }
        
        Vector2 climbVelocity = new Vector2 (rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;
        
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool(CLIMBING_ANIMATION, playerHasVerticalSpeed);
    }

    void Die()
    {
        if (myCC.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rb.velocity = deathKick;
            FindObjectOfType<GameSessions>().ProcessPlayerDeath();
        }
    }
}
