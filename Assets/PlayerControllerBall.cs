using UnityEngine;

public class PlayerControllerBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     private Rigidbody2D rb;
    private bool canJump;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    float x_input;
    bool isCharging = false;
   
   void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.linearVelocity = new Vector2(0, jumpForce);
    }

        x_input = Input.GetAxisRaw("Horizontal");
        
        if (!isCharging)
        {
            Move();
        }
       
    }

    private void Move()
    {
        if(x_input > 0)
        {
            rb.linearVelocity = Vector2.right * moveSpeed;
        } else if (x_input < 0)
        {
            rb.linearVelocity = Vector2.left * moveSpeed;
            
        } else 
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    
    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void OnCollisionExit2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}

