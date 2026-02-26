using UnityEngine;

public class UIManagerController : MonoBehaviour
{
    [SerializeField] public GameObject tutorialText;
    [SerializeField] private float timeVisable = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("HideControls", timeVisable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HideControls()
    {
        tutorialText.SetActive(false);
    }
}
