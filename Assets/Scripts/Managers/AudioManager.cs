using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    [SerializeField] private AudioSource musicSource = default;
    [SerializeField] private AudioSource sfxSource = default;

    public float MusicVolume
    {
        get => musicSource.volume;
        set
        {
            musicSource.volume = value;
            GameProfile.Music = value;
        }
    }

    public float SFXVolume
    {
        get => sfxSource.volume;
        set
        {
            sfxSource.volume = value;
            GameProfile.SFX = value;
        }
    }

    [SerializeField] private AudioClip[] musicThemes = default;

    private int musicThemeIndex = 0;

    private void Start()
    {
        PlayNextMusic();
    }

    private void PlayNextMusic()
    {
        if (musicThemes == null) return;
        musicThemeIndex = musicThemeIndex != musicThemes.Length - 1 ?
            UnityEngine.Random.Range(0, musicThemes.Length) : 0;
        
        musicSource.clip = musicThemes[musicThemeIndex];
        musicSource.Play();
    }

    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public IEnumerator FadeAudio(AudioSource audioSource, float duration, float initialVolume, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, currentTime / duration);
            Debug.Log(audioSource.volume);
            yield return null;
        }
    }
    
    public IEnumerator FadeClipTransition(AudioSource audioSource, AudioClip targetClip, float duration)
    {
        float currentTime = 0;
        float initialVolume = audioSource.volume;
        
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, 0, currentTime / duration);
            yield return null;
        }
        audioSource.clip = targetClip;
        audioSource.volume = initialVolume;
        audioSource.Play();
    }
}
