using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerBall : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    
    #region Jump Variables
    private bool canJump;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private float jumpMultiplier;
    private float chargeTime;
    private bool isCharging = false;
    #endregion

    [SerializeField] Transform tomatoTransform;
    [SerializeField] private float moveSpeed;
    float x_input;

    private bool touchingWall;
    private bool isGrabbing;

    private Vector2 lastPos;
    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip stickSound;
    [SerializeField] private AudioClip deathSound;
    public Slider JumpSlider;

    private Vector2 initialRespawnPosition;
    private Vector2 initialRespawnOffset;
    private Vector2 respawnPosition;
    private GameObject lastCloud;
   
   void Start() {
        rb = GetComponent<Rigidbody2D>();
        JumpSlider.value = 0;
        isGrabbing = true;
        lastPos = transform.position;
        audioSource = GetComponent<AudioSource>();
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        x_input = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKey(KeyCode.Space) && canJump && isGrabbing) {
            isCharging = true;
            tomatoTransform.localScale = Vector3.one * (1 - (chargeTime / maxChargeTime) / 2);
            if (chargeTime <= maxChargeTime) {
                chargeTime += Time.deltaTime * 2f;
            }
        } else
        {
            tomatoTransform.localScale = Vector3.one;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, chargeTime * jumpMultiplier);
            isCharging = false;
            canJump = false;
            chargeTime = 0f;
            isGrabbing = false;
            audioSource.PlayOneShot(jumpSound, 0.3f);
        } 

        if (Input.GetKeyDown(KeyCode.S) && touchingWall) {
            isGrabbing = true;
            lastPos = transform.position;
            audioSource.PlayOneShot(stickSound, 2f);

            respawnPosition = transform.position;
            if (lastCloud)
            {
                initialRespawnPosition = respawnPosition;
                initialRespawnOffset = lastCloud.GetComponent<CloudPlatformScript>().currOffset;
            }
        }

        Move();
        JumpSlider.value = chargeTime / maxChargeTime;
    
    }

    void FixedUpdate() {
        if (isGrabbing) {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        }
        else {
            rb.gravityScale = 1f;
        }  
    }

    private void Move() {
        if (lastCloud && isGrabbing)
        {
            respawnPosition = initialRespawnPosition + lastCloud.GetComponent<CloudPlatformScript>().currOffset - initialRespawnOffset;
            transform.position = respawnPosition;
            return;
        }

        if (isCharging || isGrabbing) {
            return;
        }

        rb.linearVelocity = new Vector2(x_input * moveSpeed, rb.linearVelocity.y);
    }

    private void Respawn()
    {
        transform.position = respawnPosition;
        isGrabbing = true;
        
    }

    void OnTriggerStay2D (Collider2D coll) {
        if (coll.gameObject.CompareTag("Spikes"))
        {
            audioSource.PlayOneShot(deathSound, .3f);
            Respawn();
        }
        else if (coll.gameObject.CompareTag("Wall")) {
            canJump = true;
            touchingWall = true;
            if (isGrabbing)
            {
                lastCloud = null;
            }
        }
        else if (coll.gameObject.CompareTag("Cloud"))
        {
            canJump = true;
            touchingWall = true;
            lastCloud = coll.gameObject;

        }
    }
    
    
    void OnTriggerExit2D (Collider2D coll) {
        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Cloud")) {
            canJump = false;
            touchingWall = false;
        }
    }
}
