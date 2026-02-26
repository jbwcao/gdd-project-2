using TMPro;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float speed;
    float boundedX = 13f;
    float spawnX = -8f;
    int dirTravel;
    
    void Start ()
    {
        speed = Random.Range(0.5f, 1.5f);
        dirTravel = Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
        if (dirTravel == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= boundedX)
            {
                transform.position = new Vector2(spawnX, transform.position.y);
            }
            
        }
        else {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= spawnX)
            {
                transform.position = new Vector2(boundedX, transform.position.y);
            }
        }
    }
}
