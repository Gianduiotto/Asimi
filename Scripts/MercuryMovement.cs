using UnityEngine;

public class MercuryMovement : MonoBehaviour {
    public float noiseScale = 1.0f;
    public float noiseSpeed = 1.0f;
    public float scaleSpeed = 1.0f;
    public float scaleRange = 0.1f;
    public float speed = 1.0f;
    public float distortionStrength = 0.1f;
    public float distortionSpeed = 1.0f;

    private Vector3 noiseOffset;
    private Vector3 initialScale;
    private Material material;

    void Start () {
        noiseOffset = new Vector3(Random.value, Random.value, Random.value);
        initialScale = transform.localScale;
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
    }

    void Update () {
        float noise = Mathf.PerlinNoise(Time.time * noiseSpeed + noiseOffset.x,
                                        Time.time * noiseSpeed + noiseOffset.y);
        Vector3 movement = new Vector3(Mathf.PerlinNoise(Time.time * noiseSpeed + noiseOffset.x,
                                                         noiseOffset.z) - 0.5f,
                                       Mathf.PerlinNoise(noiseOffset.y,
                                                         Time.time * noiseSpeed + noiseOffset.z) - 0.5f,
                                       noise - 0.5f);
        movement = movement.normalized * speed;
        transform.position += movement * Time.deltaTime;

        float scale = Mathf.PerlinNoise(Time.time * noiseSpeed + noiseOffset.z,
                                        noiseOffset.x) * scaleRange;
        transform.localScale = initialScale * (1.0f + scale);

        noiseOffset += new Vector3(Random.value, Random.value, Random.value) * Time.deltaTime * noiseScale;

        // Add distortion effect
        float distortion = Mathf.PerlinNoise(Time.time * distortionSpeed, 0.0f);
        material.SetFloat("_DistortionStrength", distortion * distortionStrength);
        material.SetFloat("_DistortionSpeed", distortionSpeed);
    }
}

