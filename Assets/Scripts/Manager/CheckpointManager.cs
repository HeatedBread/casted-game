using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    public bool isSaveActive;

    [SerializeField] Animator anim;
    [SerializeField] AudioSource audioSource;

    private LevelManager levelManager;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (anim != null)
        {
            anim.SetBool("isSaveActive", isSaveActive);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string RespawnText = string.Format("Respawning object with tag: {0}", other.gameObject.tag);
            print(RespawnText);

            levelManager.currentCheckpoint = gameObject;
            isSaveActive = true;

            if (audioSource != null)
                audioSource.PlayOneShot(audioSource.clip);

            StartCoroutine("RemoveAudio");
        }
    }

    IEnumerator RemoveAudio()
    {
        yield return new WaitForSeconds(2);
        audioSource = null;
    }
}
