using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenArea : MonoBehaviour
{

    [SerializeField] float delay;

    [SerializeField] Color oldColour, newColour;
    [SerializeField] Tilemap HiddenAreaTile;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.instance.HiddenRoom;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.Play();
        if (other.tag == "Player")
        {
            HiddenAreaTile.color = Color.Lerp(oldColour, newColour, delay);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        audioSource.Play();
        if (other.tag == "Player")
        {
            HiddenAreaTile.color = Color.Lerp(newColour, oldColour, delay);
        }
    }
}
