using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    [SerializeField] private float defaultMovementSpeed = 10f;

    public float Speed
    {
        get => IsGameStarted ? (InputManager.Instance.Boost ? defaultMovementSpeed * 2 : defaultMovementSpeed) : 0;
    }

    private bool _isGameStarted = false;
    public bool IsGameStarted
    {
        get => _isGameStarted;
        set
        {
            _isGameStarted = value;
            if (value)
            {
                GameProfile.ResetGame();
                OnGameStart?.Invoke();
            }
        }
    }
    
    private bool _isGameOver = false;
    public bool IsGameOver
    {
        get => _isGameOver;
        set
        {
            _isGameOver = value;
            if(value)
                OnGameOver?.Invoke();
        }
    }
    
    public Action OnGameStart;
    public Action OnGameOver;

    private void Update()
    {
        if(!IsGameStarted && !IsGameOver)
        {
            if (Input.anyKeyDown)
                IsGameStarted = true;
            return;
        }

        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("GameScene");
            else if (Input.GetKeyDown(KeyCode.M))
                SceneManager.LoadScene("MainMenu");
        }

        if (!IsGameOver)
            GameProfile.Time += Time.deltaTime;
    }
    
    public void GameOver()
    {
        GameProfile.CheckHighscore();
        IsGameStarted = false;
        IsGameOver = true;
    }
}
