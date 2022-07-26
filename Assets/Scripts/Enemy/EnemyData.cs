using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
    [Header("General")]
    public float Health;
    public float CurrentHealth;
    public float MoveSpeed;
    public float ChaseSpeed;

    [Header("Combat")]
    public float Damage;
    public float AttackRange;
}
