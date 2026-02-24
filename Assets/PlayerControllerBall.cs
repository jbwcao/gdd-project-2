using UnityEngine;

public class PlayerControllerBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     private Rigidbody2D rb;
    private bool canJump;

    [SerializeField] private float maxChargeTime;
    [SerializeField] private float jumpMultiplier;
    private float chargeTime;
    private bool isCharging = false;

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
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            isCharging = true;
            if (chargeTime <= maxChargeTime)
            {
                chargeTime += Time.deltaTime;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            rb.linearVelocity = new Vector2(0, chargeTime * jumpMultiplier);
            isCharging = false;
            canJump = false;
            chargeTime = 0f;
        }
            
            
    }

   
    
     void OnCollisionEnter2D (Collision2D coll) {
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

