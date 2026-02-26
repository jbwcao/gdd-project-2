using UnityEngine;
using System;

public class CloudPlatformScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveRange;
    [SerializeField] bool horizontalMovement;

    private float direction;
    private Vector2 startPosition;
    private float currPosition;
    public Vector2 currOffset;

    void Start()
    {
        currOffset = Vector2.zero;
        startPosition = transform.position;
        direction = 1;
    }

    void FixedUpdate()
    {
        currPosition += moveSpeed * direction * Time.deltaTime;
        if (Math.Abs(currPosition) > moveRange) {
            direction *= -1;
        }

        if (horizontalMovement)
        {
            currOffset = new Vector2(currPosition, 0);
            transform.position = startPosition + currOffset;
        } 
        else
        {
            currOffset = new Vector2(0, currPosition);
            transform.position = startPosition + currOffset;
        }
    }
}
