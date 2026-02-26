using TMPro;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float speed;
    [SerializeField] float resetX;
    [SerializeField] float startX;
    
    void Start ()
    {
        speed = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= resetX)
        {
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
}
