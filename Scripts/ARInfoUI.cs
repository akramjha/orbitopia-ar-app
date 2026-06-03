using UnityEngine;

public class ARInfoUI : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject soundButton;
    public AudioSource audioSource;
    public AudioClip planetSound;

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        soundButton.SetActive(true);
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        soundButton.SetActive(false);

        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public void PlaySound()
    {
        audioSource.Stop();
        audioSource.clip = planetSound;
        audioSource.Play();
    }
}