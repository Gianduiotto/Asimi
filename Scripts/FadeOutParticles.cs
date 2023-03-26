using UnityEngine;

public class FadeOutParticles : MonoBehaviour
{
    public float fadeOutDuration = 1.0f;

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    private void Update()
    {
        int particleCount = particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            float remainingLifetime = particles[i].remainingLifetime;
            float startLifetime = particles[i].startLifetime;
            float lifePercent = remainingLifetime / startLifetime;

            Color currentColor = particles[i].GetCurrentColor(particleSystem);
            currentColor.a = Mathf.Lerp(0, 1, lifePercent * fadeOutDuration);
            particles[i].startColor = currentColor;
        }

        particleSystem.SetParticles(particles, particleCount);
    }
}
