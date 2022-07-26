using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollectable : MonoBehaviour
{
    public int CollectedApples;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UIManager.instance.AddApple(CollectedApples);
    }

    private void CollectApple()
    {
        CollectedApples++;
        UIManager.instance.AddApple(CollectedApples);
        StartCoroutine(AppleCollected());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectApple();
        }
    }

    private IEnumerator AppleCollected()
    {
        audioSource.Play();
        gameObject.GetComponent<Collider2D>().enabled = false;

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 2f);
        yield return null;
    }
}
