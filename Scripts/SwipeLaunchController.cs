using UnityEngine;

public class SwipeLaunchController : MonoBehaviour
{
    public float swipeThreshold = 50f;
    public float jumpForce = 2000f;
    public float forwardForce = 1000f;
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    private Vector2 touchStart;
    private Vector2 touchEnd;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // For mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEnd = touch.position;
                float swipeDistance = Vector2.Distance(touchEnd, touchStart);

                if (swipeDistance > swipeThreshold)
                {
                    Jump();
                }
            }
        }

        // For Unity Editor
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEnd = Input.mousePosition;
            float swipeDistance = Vector2.Distance(touchEnd, touchStart);

            if (swipeDistance > swipeThreshold)
            {
                Jump();
            }
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        bool grounded = Physics.CheckSphere(transform.position, groundCheckDistance + extraHeight, groundLayer);
        return grounded;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        }
    }
}
