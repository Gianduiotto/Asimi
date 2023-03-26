using UnityEngine;
using UnityEngine.UI;

public class MobileInputController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public RectTransform joystickArea;
    public Image joystickBackground;
    public Image joystickHandle;
    public float joystickRadius = 100f;
    public float fadeOutSpeed = 5f;

    private Vector2 joystickCenter;
    private CanvasGroup joystickCanvasGroup;

    private void Start()
    {
        joystickCanvasGroup = joystickBackground.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        joystickCenter = joystickArea.position;

        bool isTouching = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                isTouching = true;

                Vector2 touchDelta = touch.position - joystickCenter;
                Vector2 clampedDelta = Vector2.ClampMagnitude(touchDelta, joystickRadius);
                joystickHandle.rectTransform.position = joystickCenter + clampedDelta;
                Vector2 moveDirection = clampedDelta.normalized;
                transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                joystickHandle.rectTransform.position = joystickCenter;
            }
        }

        if (isTouching)
        {
            joystickCanvasGroup.alpha = Mathf.Lerp(joystickCanvasGroup.alpha, 1, fadeOutSpeed * Time.deltaTime);
        }
        else
        {
            joystickCanvasGroup.alpha = Mathf.Lerp(joystickCanvasGroup.alpha, 0, fadeOutSpeed * Time.deltaTime);
        }
    }
}
