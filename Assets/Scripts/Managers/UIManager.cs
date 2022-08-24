using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("References")]
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject gameOverScreen;

    [Header("Game Screen")]
    public Text highscoreText;
    public Text scoreText;
    public Text wavesText;

    [Header("Game Over Screen")]
    public Text endHighscoreText;
    public Text endScoreText;
    public Text endWavesText;

    private GameObject currentScreen;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        HideScreen(gameScreen);
        HideScreen(gameOverScreen);
        SwitchScreen(startScreen);
    }

    public void OnGameStarted()
    {
        SwitchScreen(gameScreen);

        wavesText.text = "WAWES\n00";
        scoreText.text = "SCORE: 000000";
        highscoreText.text = "HIGH: " + SaveManager.Instance.HighScore.ToString("000000");

        GameManager.Instance.OnScoreUpdated += UpdateScore;
        GameManager.Instance.OnHighScoreBeaten += OnHighScoreBeaten;
    }

    public void OnGameFinished()
    {
        ShowScreen(gameOverScreen);

        var gameManager = GameManager.Instance;

        gameManager.OnScoreUpdated -= UpdateScore;
        gameManager.OnHighScoreBeaten -= OnHighScoreBeaten;

        var score = gameManager.Score;
        var waves = gameManager.WavesClear;
        var high = SaveManager.Instance.HighScore;

        endScoreText.text = "SCORE:\t" + score.ToString("000000");
        endWavesText.text = "WAVES:\t" + waves.ToString("00");
        endHighscoreText.text = "HIGH:\t" + high.ToString("000000");
    }

    private void UpdateScore()
    {
        var score = GameManager.Instance.Score;
        var waves = GameManager.Instance.WavesClear;
        var high = SaveManager.Instance.HighScore;

        scoreText.text = "SCORE: " + score.ToString("000000");
        wavesText.text = "WAVES\n" + waves.ToString("00");

        if (score > high)
        {
            highscoreText.text = "HIGH: " + score.ToString("000000");
        }
    }

    private void OnHighScoreBeaten()
    {
        GameManager.Instance.OnHighScoreBeaten -= OnHighScoreBeaten;
        highscoreText.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f, 20);
        highscoreText.DOColor(Color.red, 0.6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void SwitchScreen(GameObject target)
    {
        if (currentScreen != null)
        {
            HideScreen(currentScreen);
        }

        ShowScreen(target);
    }

    public void HideScreen(GameObject screen)
    {
        if (currentScreen == screen)
        {
            currentScreen = null;
        }

        screen.SetActive(false);
    }

    public void ShowScreen(GameObject screen)
    {
        currentScreen = screen;
        screen.SetActive(true);
    }
}
