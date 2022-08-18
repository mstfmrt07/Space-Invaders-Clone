using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public InvadersController invaderGrid;
    public Action OnScoreUpdated;
    public Action OnHighScoreBeaten;

    public int Score => score;
    public int TotalShots => totalShots;

    private int score;
    private int totalShots;
    private int wavesClear = 0;
    private bool highScoreBeaten = false;

    public int WavesClear => wavesClear;

    private void Awake()
    {
        //Fix the max FPS count at 60
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        InputController.Instance.IsActive = true;

        Player.Instance.Initialize();
        Player.Instance.hitbox.OnDestroy += FinishGame;
        Player.Instance.shooter.OnShoot += CountShots;

        invaderGrid.Initialize();
        invaderGrid.OnGridClear += NewWave;
        invaderGrid.OnBaseInvaded += FinishGame;

        UIManager.Instance.SwitchScreen(UIManager.Instance.gameScreen);
    }

    public void FinishGame()
    {
        InputController.Instance.IsActive = false;

        Player.Instance.ResetPlayer();
        Player.Instance.hitbox.OnDestroy -= FinishGame;
        Player.Instance.shooter.OnShoot -= CountShots;

        invaderGrid.StopInvaders();
        invaderGrid.OnGridClear -= NewWave;
        invaderGrid.OnBaseInvaded -= FinishGame;

        UIManager.Instance.ShowScreen(UIManager.Instance.gameOverScreen);

        //Save new high score if player beats the old one.
        if (score > SaveManager.Instance.HighScore)
        {
            SaveManager.Instance.HighScore = score;
        }
    }

    public void RestartGame()
    {
        DG.Tweening.DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetGame()
    {
        ResetScore();
        invaderGrid.ResetGrid();
        ClearBulletsInScene();
        totalShots = 0;
    }

    public void GainScore(int scoreToAdd)
    {
        score += scoreToAdd;
        OnScoreUpdated?.Invoke();

        if (score > SaveManager.Instance.HighScore && SaveManager.Instance.HighScore != 0 && !highScoreBeaten)
        {
            highScoreBeaten = true;
            OnHighScoreBeaten?.Invoke();
        }
    }

    private void NewWave()
    {
        invaderGrid.StopInvaders();
        wavesClear++;
        invaderGrid.Initialize();
    }

    private void ResetScore()
    {
        score = 0;
        OnScoreUpdated?.Invoke();
    }

    private void CountShots()
    {
        totalShots++;
    }

    private void ClearBulletsInScene()
    {
        var bullets = FindObjectsOfType<Projectile>();

        foreach (var bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}
