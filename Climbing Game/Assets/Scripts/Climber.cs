using UnityEngine;
using System.Collections;
public class Climber : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private string ___________; // c'est tempo c'est juste pour mieux voir
    [SerializeField] private float holdTime_Y = 2f;
    [SerializeField] private float holdTime_G = 1.5f;
    [SerializeField] private float holdTime_B = 1f;
    [SerializeField] private float holdTime_R = 0.5f;

    private string HOLDS_Y_TAG = "Yellow Holds";
    private string HOLDS_G_TAG = "Green Holds";
    private string HOLDS_B_TAG = "Blue Holds";
    private string HOLDS_R_TAG = "Red Holds";
    private string HAND_LEFT_TAG = "Hand Left";
    // private string HAND_RIGHT_TAG = "Hand Right";
    private string GROUND_TAG = "Ground";
    
    private float movementX;
    private float movementY;
    private bool canClimb = false;
    private bool canJump = false;
    private bool isHolding = false;
  
    private Rigidbody2D rb2d;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        

    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        movementY = Input.GetAxis("Vertical");
        movementX = Input.GetAxis("Horizontal");

        if (canClimb == true && canJump == false)
        {
            transform.position += new Vector3(0f, movementY, 0f) * Time.deltaTime * moveSpeed;
            transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveSpeed;
            PlayerJump();
        } 
        else if (canClimb == false)
        {
            transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveSpeed;
            PlayerJump();
        }
    }

    private void PlayerJump()
    {
        if ((canJump == true || canClimb == true) && Input.GetButtonDown("Jump"))   
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag(HAND_LEFT_TAG) &&
            (collision.CompareTag(HOLDS_Y_TAG) || collision.CompareTag(HOLDS_G_TAG) ||
             collision.CompareTag(HOLDS_B_TAG) || collision.CompareTag(HOLDS_R_TAG)))
        {
            Debug.Log("après le premier if");
            isHolding = true;
            canClimb = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            Debug.Log("gravity scale");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("gravity velocity");
        
            if (collision.CompareTag(HOLDS_Y_TAG)) 
            {   
                Debug.Log("je suis arrivé a destination");
                StartCoroutine(HoldingTime(holdTime_Y, HOLDS_Y_TAG));
            } 
            else if (collision.CompareTag(HOLDS_G_TAG)) 
            {
                StartCoroutine(HoldingTime(holdTime_G, HOLDS_G_TAG)); 
            }
            else if (collision.CompareTag(HOLDS_B_TAG)) 
            {
                StartCoroutine(HoldingTime(holdTime_B, HOLDS_B_TAG)); 
            }
            else if (collision.CompareTag(HOLDS_R_TAG)) 
            {
                StartCoroutine(HoldingTime(holdTime_R, HOLDS_R_TAG));
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canClimb = false;
        rb2d.gravityScale = 1;
        // Destroy(collision.gameObject, 2f); // TEMPORAIRE A MODIFIER (après la coroutine)
        if (collision.CompareTag(HOLDS_Y_TAG) || collision.CompareTag(HOLDS_G_TAG) ||
            collision.CompareTag(HOLDS_B_TAG) || collision.CompareTag(HOLDS_R_TAG))
        {
            isHolding = false;
            StopAllCoroutines();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            canJump = true;
        }
    }

    private IEnumerator HoldingTime(float time, string tag)
    {
        if (isHolding)
        {
            yield return new WaitForSeconds(time);
            canClimb = false;
            rb2d.gravityScale = 1;
            Debug.Log("Temps écoulé pour la prise de couleur : " + tag);
            isHolding = false;
        }
    }
}