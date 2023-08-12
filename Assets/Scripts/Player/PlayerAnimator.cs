using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    private PlayerController player;
    private PlayerHealth pHealth;

    public static PlayerAnimator instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<PlayerController>();
        pHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        anim.SetBool("isMoving", player.moveDirection != 0);

        anim.SetBool("isGrounded", player.isGrounded);

        anim.SetBool("canDoubleJump", player.canDoubleJump);

        anim.SetBool("onPlayerReset", pHealth.playerReset);


        anim.SetFloat("YVel", player.rigid.velocity.y);
        anim.SetBool("onLevelComplete", GoalManager.instance.onLevelComplete);
    }
}
