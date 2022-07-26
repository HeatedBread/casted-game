using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Player Audio")]
    public AudioClip PlayerJumpAudio;
    public AudioClip PlayerFallAudio;
    public AudioClip PlayerHitAudio;

    [Header("Enemy Audio")]
    public AudioClip EnemyAttack;

    [Header("Misc")]
    public AudioClip HiddenRoom;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

}
