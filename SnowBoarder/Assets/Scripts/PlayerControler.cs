using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 40f;
    [SerializeField] private float baseSpeed = 30f;    
    
    private Rigidbody2D rb2d;
    private SurfaceEffector2D surfaceEffector2d;
    private bool canMove = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2d = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2d.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2d.speed = baseSpeed;
        }
    }
}    
    
