using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Text highscoreCounter = default;

    [Header("Buttons")]
    [SerializeField] private AudioClip buttonSound = default;
    [SerializeField] private UI_Button[] buttons = default;
    
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel = default;
    [SerializeField] private GameObject settingsPanel = default;

    [Header("Settings")]
    [SerializeField] private Slider musicSlider = default;
    [SerializeField] private Slider sfxSlider = default;

    private void Start()
    {
        InitializeButtons();
        InitializePanels();
        ConfigureSettingsControls();
    }

    private void InitializeButtons()
    {
        buttons = GetComponentsInChildren<UI_Button>();
        foreach (var btn in buttons)
        {
            btn.OnClick.AddListener(PlayButtonSound);
        }
    }
    
    private void InitializePanels()
    {
        highscoreCounter.text = "Best Score: " + GameProfile.Highscore.ToString();
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    private void ConfigureSettingsControls()
    {
        // Audio
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        musicSlider.onValueChanged.Invoke(GameProfile.Music);
        musicSlider.value = GameProfile.Music;

        sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
        sfxSlider.onValueChanged.Invoke(GameProfile.SFX);
        sfxSlider.value = GameProfile.SFX;
    }

    #region Settings

    private void UpdateMusicVolume(float volume)
    {
        AudioManager.Instance.MusicVolume = volume;
    }

    private void UpdateSFXVolume(float volume)
    {
        AudioManager.Instance.SFXVolume = volume;
    }

    #endregion
    
    #region Button Callbacks
    
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenMainMenu()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayOneShot(buttonSound, 1f);
    }

    #endregion

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(UpdateMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(UpdateSFXVolume);
    }
}
