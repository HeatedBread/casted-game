using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    [SerializeField] float WaitTime;

    [SerializeField] string SceneToLoad;

    public bool onLevelComplete;

    private LevelManager levelManager;
    private PlayerController player;

    public static GoalManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        LevelComplete();
    }

    private void LevelComplete()
    {
        if (onLevelComplete)
        {
            StartCoroutine("SceneTransition");
            player.SetPlayerCanMove(false);
            player.rigid.velocity = new Vector2(player.Speed(), 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
            onLevelComplete = true;
    }

    public IEnumerator SceneTransition()
    {
        if (levelManager.fadeToBlack != null)
        {
            Instantiate(levelManager.fadeToBlack, Vector3.zero, Quaternion.identity);
            levelManager.fadeToBlack = null;
        }

        yield return new WaitForSeconds(WaitTime);

        for (int i = 0; i < 1; i++)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
            onLevelComplete = false;

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
