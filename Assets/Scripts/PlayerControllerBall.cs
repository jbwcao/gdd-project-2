using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     private Rigidbody2D rb;
    private bool canJump;

    [SerializeField] private float maxChargeTime;
    [SerializeField] private float jumpMultiplier;
    private float chargeTime;
    private bool isCharging = false;
    [SerializeField] private float moveSpeed;
    float x_input;
    private bool touchingWall;
    public Slider JumpSlider;
    private bool isGrabbing;
    
   
   void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpSlider.value = 0;
    }

    // Update is called once per frame
    void Update() 
    {
        x_input = Input.GetAxisRaw("Horizontal");
        
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, chargeTime * jumpMultiplier);
            isCharging = false;
            canJump = false;
            chargeTime = 0f;
            isGrabbing = false;
            
        } 
        if (Input.GetKey(KeyCode.S) && touchingWall)
        {
            isGrabbing = true;
        }

        Move();
        JumpSlider.value = chargeTime / maxChargeTime;
    
    }
    void FixedUpdate()
    {
        if (isGrabbing)
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
    else
    {
        rb.gravityScale = 1f;
    }
        
    }

    private void Move()
{
    if (isCharging || isGrabbing)
        {
            return;
        }
        
    rb.linearVelocity = new Vector2(x_input * moveSpeed, rb.linearVelocity.y);
    
}
    
    void OnTriggerStay2D (Collider2D coll) {
        
        if (coll.gameObject.CompareTag("Wall"))
        {
            canJump = true;
            touchingWall = true;
        }
    }
    
    
    void OnTriggerExit2D (Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            canJump = false;
            touchingWall = false;
        }
    }
}

