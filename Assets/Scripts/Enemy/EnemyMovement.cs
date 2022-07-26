using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] 
    public float CurrentSpeed;

    private Rigidbody2D rigid;
    private EnemyCollision eCollision;
    [SerializeField] EnemyData eData;

    private void Start()
    {
        InitializeVariables();
    }


    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void InitializeVariables()
    {
        rigid = GetComponent<Rigidbody2D>();
        eCollision = GetComponent<EnemyCollision>();
        CurrentSpeed = eData.MoveSpeed;
    }

    private void ApplyMovement()
    {
        float velX = CurrentSpeed;

        if (eCollision.isFacingLeft) {
            velX = -CurrentSpeed;
        } else {
            velX = CurrentSpeed;
        }

        rigid.velocity = new Vector2(velX, rigid.velocity.y);
    }
}
