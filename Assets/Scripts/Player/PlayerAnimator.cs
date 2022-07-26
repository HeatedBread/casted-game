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
        if (player.MoveDirection() != 0) {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }

        if (player.IsGrounded())
        {
            anim.SetBool("isGrounded", true);
        }
        else {
            anim.SetBool("isGrounded", false);
        }

        if (!player.IsGrounded())
        {
            anim.SetBool("canDoubleJump", true);
        }
        else {
            anim.SetBool("canDoubleJump", true);
        }

        if (pHealth.onPlayerReset)
        {
            anim.SetBool("onPlayerReset", true);
        }
        else {
            anim.SetBool("onPlayerReset", false);
        }

        anim.SetFloat("YVel", player.rigid.velocity.y);
        anim.SetBool("onLevelComplete", GoalManager.instance.onLevelComplete);
    }
}
