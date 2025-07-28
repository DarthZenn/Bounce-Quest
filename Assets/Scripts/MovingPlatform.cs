using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;

    private float screenLeft;
    private float screenRight;
    private float platformHalfWidth;
    private float timeOffset;
    private float verticalY;

    public float minWidth;
    public float maxWidth;

    private float directionMultiplier;

    public bool scored = false;
    public bool isFrozen = false;

    public void Initialize()
    {
        float randomWidth = Random.Range(minWidth, maxWidth);
        transform.localScale = new Vector3(randomWidth, transform.localScale.y, transform.localScale.z);

        verticalY = transform.position.y;

        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = screenHalfHeight * Screen.width / (float)Screen.height;

        platformHalfWidth = GetComponent<Renderer>().bounds.size.x / 2f;

        screenLeft = -screenHalfWidth + platformHalfWidth;
        screenRight = screenHalfWidth - platformHalfWidth;

        directionMultiplier = Random.value < 0.5f ? -1f : 1f;

        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        if (isFrozen) return;

        float t = Mathf.PingPong((Time.time + timeOffset) * moveSpeed, 1f);
        float x = Mathf.Lerp(screenLeft, screenRight, t);
        transform.position = new Vector3(x * directionMultiplier, verticalY, transform.position.z);
    }
}
