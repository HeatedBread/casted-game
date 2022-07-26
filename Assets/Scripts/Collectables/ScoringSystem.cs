using UnityEngine;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ValueText;

    public int ItemsCollected;

    private void Update()
    {
        Collect();
    }

    private void Collect()
    {

        string temp = string.Format("x{0}", ItemsCollected);
        ValueText.text = temp;
    }
}
