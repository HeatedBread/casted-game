using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    [SerializeField] EnemyCombat eAttack;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        eAttack = GetComponent<EnemyCombat>();
    }

    private void Update()
    {
        if (rigid.velocity.x != 0) {
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }

        if (eAttack.isAttacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else {
            anim.SetBool("isAttacking", false);
        }
    }

}
