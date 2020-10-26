using System;
using UnityEngine;

public class GameProfile
{
    private static int _score = 0;

    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChange?.Invoke(value);
        }
    }

    private static int _asteroids = 0;
    public static int Asteroids
    {
        get => _asteroids;
        set
        {
            _asteroids = value;
            OnAsteroidsChange?.Invoke(value);
        }
    }

    private static float _time = 0;
    public static float Time
    {
        get => _time;
        set
        {
            _time = value;
            OnTimeChange?.Invoke(value);
        }
    }

    public static string GetTimeString()
    {
        string minutes = ((int)(Time / 60f)).ToString("00");
        string seconds = ((int)(Time % 60f)).ToString("00");
        return minutes + ":" + seconds;
    }

    public static int Highscore
    {
        get => PlayerPrefs.GetInt("Highscore", 0);
        set => PlayerPrefs.SetInt("Highscore", value);
    }

    public static void ResetGame()
    {
        Score = 0;
        Asteroids = 0;
        Time = 0;
    }

    public static void CheckHighscore()
    {
        if (Score > Highscore)
        {
            Highscore = Score;
            OnNewHighscore?.Invoke(Highscore);
        }
    }

    public static Action<int> OnScoreChange;
    public static Action<int> OnAsteroidsChange;
    public static Action<float> OnTimeChange;
    public static Action<int> OnNewHighscore;
}
