using UnityEngine;

public class pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject player;

    
    public void toggle()
    {
        //its been clicked again so now lets change the bool
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        player.GetComponent<PlayerMovement>().enabled = !isPaused;
    }
}
