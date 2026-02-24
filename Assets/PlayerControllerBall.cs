using UnityEngine;

public class PlayerControllerBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     private Rigidbody2D rb;
    private bool canJump;
    [SerializeField] private float jumpForce;
   
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

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.GameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void OnCollisionExit2D (Collsion2D coll)
    {
        if (coll.GameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
    }
}

