using UnityEngine;

public class toggleText : MonoBehaviour
{
    public GameObject canvas;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Player"))
        {
             Debug.Log("Active");
            canvas.SetActive(true);
        }

    }

     void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }

    }
}
