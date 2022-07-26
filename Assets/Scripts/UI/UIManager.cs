using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private PlayerHealth pHealth;

    [Header("Hearts")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Lives")]
    [SerializeField] GameObject LifeIconPrefab;
    [SerializeField] Transform LifeHolder;
    private Stack<GameObject> lives = new Stack<GameObject>();

    [Header("Apples")]
    public GameObject AppleIconPrefab;
    public Transform AppleHolder;
    private Stack<GameObject> apples = new Stack<GameObject>();

    private void Awake()
    {
        instance = this;
        pHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        HeartManager();
    }

    private void HeartManager()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < pHealth.Health())
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < pHealth.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void AddApple(int _apples)
    {
        for (int i = 0; i < _apples; i++)
        {
            apples.Push(Instantiate(AppleIconPrefab, AppleHolder));
        }
    }

    public void AddLife(int _lives)
    {
        for (int i = 0; i < _lives; i++)
        {
           lives.Push(Instantiate(LifeIconPrefab, LifeHolder));
        }
    }

    public void RemoveLife()
    {
        if(lives != null)
            Destroy(lives.Pop());
    }
}
