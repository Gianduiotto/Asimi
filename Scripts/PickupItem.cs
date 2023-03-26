using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public PickupTextController pickupTextController;
    public GameObject explosionPrefab;
    public AudioClip pickupSound;
    public string pickupMessage = "Picked up an item";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the explosion effect at the item's position
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Show the pickup text
            pickupTextController.ShowPickupText(pickupMessage);

            // Play the pickup sound
            PlayPickupSound();

            // Destroy the item and the explosion effect after a delay
            Destroy(gameObject, 0.5f); // Adjust the delay as needed
            Destroy(explosion, explosionPrefab.GetComponent<ParticleSystem>().main.duration);
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            GameObject tempAudioSource = new GameObject("TempAudioSource");
            AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();
            audioSource.clip = pickupSound;
            audioSource.Play();
            Destroy(tempAudioSource, pickupSound.length);
        }
    }
}
