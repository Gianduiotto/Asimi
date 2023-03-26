using UnityEngine;

public class LiquidMercury : MonoBehaviour
{
    [SerializeField] private Material mercuryMaterial;
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float frequency = 1f;

    private Vector3 originalPosition;
    private float offset;

    private void Start()
    {
        originalPosition = transform.position;
        offset = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update()
    {
        float y = originalPosition.y + amplitude * Mathf.Sin(frequency * Time.time + offset);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void OnRenderObject()
    {
        mercuryMaterial.SetPass(0);
    }
}
