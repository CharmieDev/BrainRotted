using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayers;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 40.0f;
    public float dashSpeed = 10.0f;
    public float gravityScale = 10.0f;
    private const float gravityValue = -9.81f;

    private bool isMovingRight = true;
    private bool isGrounded;
    private bool touchedGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        InputMove();
        FlipPlayer();
        InputJump();

        if (rb.linearVelocityX == 0)
        {
            rb.linearVelocityX = 0;
        }

        rb.linearVelocityY += gravityValue * gravityScale * Time.fixedDeltaTime;
        Debug.Log("Grounded:" + isGrounded);
    }

    public void InputMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocityX = playerSpeed;
            isMovingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocityX = playerSpeed * -1;
            isMovingRight = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            touchedGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public void InputJump()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded == true)
        {
            rb.linearVelocityY = jumpHeight;
        }
    }

    void Dash()
    {
        if (touchedGround = true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.linearVelocityX = dashSpeed;
            }
        }
    }

    // Changes the Player to look left or right
    void FlipPlayer()
    {
        // Changes the direction the Player is facing
        if (isMovingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    }
    
}
