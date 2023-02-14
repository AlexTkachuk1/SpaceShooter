using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartCoopGame()
    {
        SceneManager.LoadScene(2);
    }
}
