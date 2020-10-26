using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Text highscoreCounter = null;

    private void Start()
    {
        highscoreCounter.text = "Best Score: " + GameProfile.Highscore.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();        
    }
}
