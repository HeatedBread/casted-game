using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Death")]
    public float stunTime;
    public float knockbackDistance;
    public float deathTime;
    public bool playerReset;

    [Header("Health")]
    private int currentHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private int liveRemaining;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        currentHealth = maxHealth;

        UIManager.instance.AddLife(liveRemaining);
    }

    private void Update()
    {
        HealthManager();
        Die();
    }

    private void HealthManager()
    {
        if (liveRemaining <= 0) {
            liveRemaining = 0;
        }
        
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }


        if (currentHealth <= 0) {
            playerReset = true;
        } else {
            playerReset = false;
        }
            
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        PlayerController.instance.audioSource.clip = AudioManager.instance.PlayerHitAudio;
        PlayerController.instance.audioSource.Play();
        StartCoroutine(PlayerHurt());
    }

    private void RemoveLife()
    {
        liveRemaining--;
        UIManager.instance.RemoveLife();
    }

    private void Die()
    {
        if (playerReset && liveRemaining > 0)
        {
            RemoveLife();
            StartCoroutine(PlayerDeath());
        }
        else if (liveRemaining < 1)
        {
            playerReset = false;
            Destroy(gameObject);
            StartCoroutine(GoalManager.instance.SceneTransition());
        }
    }

    public float SetHealth(int health) => currentHealth = health;
    public float GetMaxHealth() => maxHealth;
    public float Health() => currentHealth;

    private IEnumerator PlayerDeath()
    {
        currentHealth = maxHealth;
        levelManager.RespawnPlayer();
        yield return null;
    }

    private IEnumerator PlayerHurt()
    {
        Rigidbody2D pRigid = gameObject.GetComponent<Rigidbody2D>();
        if (PlayerController.instance.MoveDirection() > 0)
        {
            pRigid.AddForce(new Vector2(gameObject.transform.forward.x - knockbackDistance, PlayerController.instance.isGrounded ? knockbackDistance * 7.5f : knockbackDistance), ForceMode2D.Impulse);
        }
        else {
            pRigid.AddForce(new Vector2(gameObject.transform.forward.x + knockbackDistance, PlayerController.instance.isGrounded ? knockbackDistance * 7.5f : knockbackDistance), ForceMode2D.Impulse);
        }

        PlayerController.instance.SetPlayerCanMove(false);
        PlayerAnimator.instance.anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(stunTime);
        PlayerAnimator.instance.anim.SetBool("isHurt", false);
        PlayerController.instance.SetPlayerCanMove(true);
    }

    
}
