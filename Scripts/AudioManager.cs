using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusicSource;
    public AudioSource sfxSource;
    public AudioClip buttonClickSfx;

    public bool IsMusicMuted { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgMusicSource.loop = true;

        IsMusicMuted = PlayerPrefs.GetInt("MusicOn", 1) == 0;
        bgMusicSource.mute = IsMusicMuted;

        if (!bgMusicSource.isPlaying)
            bgMusicSource.Play();
    }

    public void MusicOn()
    {
        IsMusicMuted = false;
        bgMusicSource.mute = false;
        PlayerPrefs.SetInt("MusicOn", 1);
    }

    public void MusicOff()
    {
        IsMusicMuted = true;
        bgMusicSource.mute = true;
        PlayerPrefs.SetInt("MusicOn", 0);
    }

    public void PlayButtonClick()
    {
        if (sfxSource != null && buttonClickSfx != null)
            sfxSource.PlayOneShot(buttonClickSfx);
    }
}