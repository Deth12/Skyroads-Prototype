using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header(("Panels"))] 
    [SerializeField] private GameObject startPanel = default;
    [SerializeField] private GameObject statisticsPanel = default;
    [SerializeField] private GameObject endgamePanel = default;
    [SerializeField] private GameObject congratsPanel = default;

    [Header("Counters")] 
    [SerializeField] private Text scoreCounter = default;
    [SerializeField] private Text highscoreCounter = default;
    [SerializeField] private Text asteroidsCounter = default;
    [SerializeField] private Text timeCounter = default;
    [SerializeField] private Text newHighscoreCounter = default;
    [SerializeField] private Text finalScoreCounter = default;
    [SerializeField] private Text finalAsteroidsCounter = default;
    [SerializeField] private Text finalTimeCounter = default;

    private void Start()
    {
        GameManager.Instance.OnGameStart += ShowGameUI;
        GameManager.Instance.OnGameOver += ShowEndgameUI;
        GameProfile.OnScoreChange += UpdateScoreCounter;
        GameProfile.OnAsteroidsChange += UpdateAsteroidsCounter;
        GameProfile.OnTimeChange += UpdateTimeCounter;
        GameProfile.OnNewHighscore += ShowHighscoreCongrats;
    }
    
    private void ShowGameUI()
    {
        QualityManager.Instance.SmoothToggleDOF(false);
        startPanel.SetActive(false);
        statisticsPanel.SetActive(true);
        highscoreCounter.text = GameProfile.Highscore.ToString();
    }

    private void UpdateScoreCounter(int value)
    {
        //scoreCounter.text = value.ToString();
        scoreCounter.text = value.ToFormatedScore();
        if (GameProfile.Score > GameProfile.Highscore)
            highscoreCounter.text = scoreCounter.text;
    }
    
    private void UpdateAsteroidsCounter(int value)
    {
        asteroidsCounter.text = value.ToString();
    }

    private void UpdateTimeCounter(float value)
    {
        timeCounter.text = Helper.SecondsToTimeString(value);
    }

    private void ShowEndgameUI()
    {
        QualityManager.Instance.SmoothToggleDOF(true);
        statisticsPanel.SetActive(false);
        endgamePanel.SetActive(true);
        finalScoreCounter.text = scoreCounter.text;
        finalAsteroidsCounter.text = asteroidsCounter.text;
        finalTimeCounter.text = timeCounter.text;
    }

    private void ShowHighscoreCongrats(int highscore)
    {
        congratsPanel.SetActive(true);
        newHighscoreCounter.text = highscore.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStart -= ShowGameUI;
        GameManager.Instance.OnGameOver -= ShowEndgameUI;
        GameProfile.OnScoreChange -= UpdateScoreCounter;
        GameProfile.OnAsteroidsChange -= UpdateAsteroidsCounter;
        GameProfile.OnTimeChange -= UpdateTimeCounter;
        GameProfile.OnNewHighscore -= ShowHighscoreCongrats;
    }
}
