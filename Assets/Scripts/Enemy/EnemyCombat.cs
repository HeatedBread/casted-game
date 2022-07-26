using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public bool isAttacking;

    private AudioSource audioSource;

    private EnemyMovement eMovement;
    private EnemyData eData;
    private PlayerHealth player;

    private void Start()
    {
        eMovement = GetComponent<EnemyMovement>();
        player = GameObject.Find("-- PLAYER --").GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.instance.EnemyAttack;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (isAttacking)
        {
            StartCoroutine("Attacking");
        }
        else
        {
            isAttacking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            audioSource.Play();
            player.TakeDamage(1);
        }
    }

    IEnumerator Attacking()
    {
        eMovement.CurrentSpeed = 0;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
