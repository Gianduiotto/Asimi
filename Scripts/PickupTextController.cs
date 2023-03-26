using System.Collections;
using UnityEngine;
using TMPro;

public class PickupTextController : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public float fadeInTime = 1f;
    public float displayTime = 2f;
    public float fadeOutTime = 1f;
    public float vibrationAmplitude = 1f;
    public float vibrationFrequency = 2f;
    public float fadeFrequency = 5f;
    private float timer;
    private Coroutine fadeCoroutine;
    private Vector3 initialTextPosition;

    void Start()
    {
        if (pickupText != null)
        {
            Color textColor = pickupText.color;
            textColor.a = 0f;
            pickupText.color = textColor;
            initialTextPosition = pickupText.rectTransform.localPosition;
        }
    }

    public void ShowPickupText(string newText)
    {
        pickupText.text = newText;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        // Fade in
        timer = 0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            pickupText.color = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, alpha);
            yield return null;
        }

        // Display with vibration and fading effect
        timer = 0f;
        while (timer < displayTime)
        {
            timer += Time.deltaTime;

            // Vibration effect
            float x = initialTextPosition.x + Random.Range(-vibrationAmplitude, vibrationAmplitude);
            float y = initialTextPosition.y + Random.Range(-vibrationAmplitude, vibrationAmplitude);
            pickupText.rectTransform.localPosition = new Vector3(x, y, initialTextPosition.z);

            // Sinusoidal fading effect
            float fade = Mathf.Sin(timer * fadeFrequency) * 0.5f + 0.5f;
            pickupText.color = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, fade);

            yield return null;
        }

        // Fade out
        timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutTime);
            pickupText.color = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, alpha);
            yield return null;
        }

        // Reset the alpha to 0
        pickupText.color = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, 0f);
        pickupText.rectTransform.localPosition = initialTextPosition;
    }
}
