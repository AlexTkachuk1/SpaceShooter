using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    private string _gameMode;
    [SerializeField]
    private GameObject uiManager;

    private void Start()
    {
        SetGameMode();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(_gameMode);//Current Game Scene
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameMode == "Mein_Menu") Application.Quit();
            else GoToMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
            uiManager.GetComponent<UIManager>().setPauseState();
        }
    }

    private void SetGameMode()
    {
        _gameMode = SceneManager.GetActiveScene().name;
    }
    public string GetGameMode()
    {
        return _gameMode;
    }
    public void GameOver() => _isGameOver = true;

    public void GoToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Mein_Menu");
    }
    private void PauseGame()
    {
        uiManager.GetComponent<UIManager>().TogglePausMenu(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        uiManager.GetComponent<UIManager>().TogglePausMenu(false);
        Time.timeScale = 1f;
    }
}
