using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerController player;

    public GameObject currentCheckpoint;

    public GameObject slideFromBlack;
    public GameObject slideToBlack;
    public GameObject fadeToBlack;

    private void Awake()
    {
        if (slideFromBlack != null)
        {
            GameObject panel = Instantiate(slideFromBlack, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 3);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(FreezePlayerPos());
    }

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }

    IEnumerator FreezePlayerPos()
    {
        player.SetPlayerCanMove(false);
        yield return new WaitForSeconds(1.5f);
        player.SetPlayerCanMove(true);
    }
}
