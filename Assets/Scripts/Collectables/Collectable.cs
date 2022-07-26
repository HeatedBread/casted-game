using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    [SerializeField] float DelayTime;

    [SerializeField] string UIComponent;

    private bool onCollected;

    private Animator anim;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip collectSound;
    
    [SerializeField] ScoringSystem score;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        score = GameObject.Find(UIComponent).GetComponent<ScoringSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Collected();
    }

    private void Collected()
    {
        if (onCollected)
        {
            StartCoroutine("CollectedItem");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onCollected = true;
            score.ItemsCollected++;
            audioSource.PlayOneShot(collectSound);
        }
        else
            onCollected = false;
    }

    private IEnumerator CollectedItem()
    {
        anim.SetBool("onCollected", onCollected);
        gameObject.GetComponent<Collider2D>().enabled = false;
        
        yield return new WaitForSeconds(DelayTime);

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 2f);
    }
}
