using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private int _bestScore;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestScoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private Sprite[] _liveSprites;
    private int _numberOfPlayers = 0;
    private Animator _pauseAnimator;

    void Start()
    {
        _bestScore = GetBestScore();
        UpdateBestScoreValue(_bestScore);
        UpdateScoreValue(0);
        _gameOverText.gameObject.SetActive(false);
        _pauseAnimator = GameObject.Find("Pause_Menu").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void UpdateScoreValue(int gamePoints) => _scoreText.text = $"Score: {gamePoints}";
    public void UpdateBestScoreValue(int gamePoints) => _bestScoreText.text = $"Best: {gamePoints}";
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }
    void AddGameOverText()
    {
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlikerRoutine());
    }
    void AddRestartText()
    {
        _restartText.gameObject.SetActive(true);
    }
    IEnumerator GameOverFlikerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(!_gameOverText.IsActive());
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void IncreaseScore(int gamePoints)
    {
        _score += gamePoints;
        UpdateScoreValue(_score);
    }
    public void CheckForBestScore()
    {
        if (_bestScore < _score) _bestScore = _score;
        UpdateBestScoreValue(_bestScore);
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", _bestScore);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }

    public void increaseNumberOfPlayers()
    {
        _numberOfPlayers++;
    }
    public void decreaseNumberOfPlayers()
    {
        if (_numberOfPlayers >= 1) _numberOfPlayers--;
        if (_numberOfPlayers == 0)
        {
            AddRestartText();
            AddGameOverText();
        }
    }
    public void ResumeGame()
    {
        Debug.LogWarning("ResumeGame");
    }

    public void TogglePausMenu(bool isPaused)
    {
        _panel.SetActive(isPaused);
    }

    public void setPauseState()
    {
        _pauseAnimator.SetBool("isPaused", true);
    }
}
