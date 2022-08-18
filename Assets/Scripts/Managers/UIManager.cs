using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
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

        scoreText.text = "SCORE: 000000";
        highscoreText.text = "HIGH: " + SaveManager.Instance.HighScore.ToString("000000");

        GameManager.Instance.OnScoreUpdated += UpdateScore;
        GameManager.Instance.OnHighScoreBeaten += OnHighScoreBeaten;
    }

    private void UpdateScore()
    {
        var score = GameManager.Instance.Score;
        var high = SaveManager.Instance.HighScore;

        scoreText.text = "SCORE: " + score.ToString("000000");

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
