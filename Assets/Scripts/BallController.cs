using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;

    private ContactPoint2D[] contactPoints = new ContactPoint2D[4];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded && IsTouchOnGameArea())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jumpSFX);
        }

        if (isGrounded)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            int contactCount = collision.GetContacts(contactPoints);
            float ballBottom = transform.position.y - GetComponent<Collider2D>().bounds.extents.y;

            for (int i = 0; i < contactCount; i++)
            {
                if (contactPoints[i].point.y <= ballBottom + 0.1f)
                {
                    isGrounded = true;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.landedSFX);

                    MovingPlatform platformScript = collision.collider.GetComponent<MovingPlatform>();
                    if (platformScript != null)
                    {
                        platformScript.isFrozen = true;

                        if (!platformScript.scored)
                        {
                            platformScript.scored = true;
                            GameManager.Instance.AddScore(100);
                        }
                    }

                    break;
                }
            }
        }
        else if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private bool IsTouchOnGameArea()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return false;

                return true;
            }
        }
        return false;
    }
}
