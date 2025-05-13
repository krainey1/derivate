using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoDeath(){
        SceneManager.LoadScene("DeathScreen");
    }
}
