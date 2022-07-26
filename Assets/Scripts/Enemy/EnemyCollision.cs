using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private const string LEFT = "Left";
    private const string RIGHT = "Right";
    private string FacingDirection;

    public float BaseCastDistance;
    public bool isFacingLeft;

    [SerializeField] Transform castPos;
    [SerializeField] GameObject enemySprite;
    [SerializeField] GameObject player;

    private Vector3 BaseScale;

    private EnemyMovement eMovement;
    public EnemyData eData;

    private void Start()
    {
        player = GameObject.Find("-- PLAYER --");
        eMovement = GetComponent<EnemyMovement>();

        FacingDirection = RIGHT;
        BaseScale = enemySprite.transform.localScale;
    }

    private void Update()
    {
        DetectPlayer();
        DetectDirection();
    }

    private void FixedUpdate()
    {
        DetectCollision();
    }

    private void DetectDirection()
    {
        if (FacingDirection == LEFT)
        {
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
        }
    }

    private void DetectCollision()
    {
        if (IsHittingWall() || IsNearEdge())
        {
            if (FacingDirection == LEFT)
            {
                Flip(RIGHT);
            }
            else if (FacingDirection == RIGHT)
            {
                Flip(LEFT);
            }
        }
    }

    private void DetectPlayer()
    {
        // Distance to player
        float distToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distToPlayer < eData.AttackRange && PlayerController.instance.canMove)
        {
            Debug.DrawLine(gameObject.transform.position, player.transform.position, Color.blue);
            ChasePlayer();
        }
        else if (distToPlayer > eData.AttackRange || !PlayerController.instance.canMove)
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            eMovement.CurrentSpeed = eData.ChaseSpeed;
            Flip(RIGHT);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            eMovement.CurrentSpeed = eData.ChaseSpeed;
            Flip(LEFT);
        }
        else if(IsNearEdge())
        {
            StopChasingPlayer();
        }
    }

    private void StopChasingPlayer()
    {
        eMovement.CurrentSpeed = eData.MoveSpeed;
    }

    private void Flip(string newDirection)
    {
        Vector3 newScale = BaseScale;

        if (newDirection == LEFT)
            newScale.x = -BaseScale.x;
        else if (newDirection == RIGHT)
            newScale.x = BaseScale.x;

        transform.localScale = newScale;

        FacingDirection = newDirection;
    }

    bool IsHittingWall()
    {
        bool val = false;

        float castDist = BaseCastDistance;

        if (FacingDirection == LEFT)
        {
            castDist = -BaseCastDistance;
        }
        else
        {
            castDist = BaseCastDistance;
        }

        // Determine target destination based on cast distance
        Vector3 targetCastPos = castPos.position;
        targetCastPos.x += castDist;

        Debug.DrawLine(castPos.position, targetCastPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetCastPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool IsNearEdge()
    {
        bool val = true;

        float castDist = BaseCastDistance;

        // Determine pit destination based on cast distance
        Vector3 targetCastPos = castPos.position;
        targetCastPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetCastPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetCastPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
