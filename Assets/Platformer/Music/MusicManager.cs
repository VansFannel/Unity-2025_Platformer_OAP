using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private float menuMusicVolume;
    [SerializeField] private float gameplayMusicVolume;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.volume = menuMusicVolume;
        audioSource.Play();
    }

    public void PlayGameplayMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.volume = gameplayMusicVolume;
        audioSource.Play();
    }
}
