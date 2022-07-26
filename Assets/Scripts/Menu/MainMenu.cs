using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public float transitionTime;

    public GameObject disclaimer;
    public AudioSource audioSource;

    public static MainMenu instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(disclaimer != null)
        Destroy(disclaimer, 7);
    }

    public void StartGame()
    {
        audioSource.Play();
        StartCoroutine("SceneTransition");
    }


    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(transitionTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ForestZone");

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}