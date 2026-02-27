using UnityEngine;

public class StarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll)
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<GameManager>().WinGame();
        }
    }
}
