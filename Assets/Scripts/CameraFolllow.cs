using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public float verticalOffset;

    void LateUpdate()
    {
        float targetY = target.position.y + verticalOffset;

        if (targetY > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothed;
        }
    }
}