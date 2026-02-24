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
    [SerializeField] private float moveSpeed;
    float x_input;
   
   void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() 
    {
         x_input = Input.GetAxisRaw("Horizontal");
        Move();
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
        } 
    }

    private void Move()
{
    if (isCharging)
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        return;
    }
    rb.velocity = new Vector2(x_input * moveSpeed, rb.velocity.y);
}
    
    void OnTriggerEnter2D (Collider2D coll) {
        x_input = Input.GetAxisRaw("Horizontal");
        if (coll.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
    }
    
    void OnTriggerExit2D (Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}

