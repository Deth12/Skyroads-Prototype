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
    
    [SerializeField] private AudioClip[] musicThemes = null;
    
    private int musicThemeIndex = 0;
    private AudioSource src;

    private void Start()
    {
        src = GetComponent<AudioSource>();
        PlayNextMusic();
    }

    public void PlayNextMusic()
    {
        if (musicThemes == null) return;
        musicThemeIndex = musicThemeIndex != musicThemes.Length - 1 ?
            UnityEngine.Random.Range(0, musicThemes.Length) : 0;
        src.clip = musicThemes[musicThemeIndex];
        src.Play();
    }

    public void PlayOneShot(AudioClip clip, float volume)
    {
        src.PlayOneShot(clip, volume);
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
