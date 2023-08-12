using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private PlayerHealth pHealth;
    private AudioSource audioSource;

    private void Start()
    {
        pHealth = FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.instance.PlayerFallAudio;
    }

    private void KillPlayer()
    {
        pHealth.SetHealth(0);
        pHealth.playerReset = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.Play();

        if (other.gameObject.CompareTag("Player"))
        {
            KillPlayer();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject, 1);
        }
    }
}
