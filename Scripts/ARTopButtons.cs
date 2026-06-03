using UnityEngine;
using UnityEngine.UI;

public class ARTopButtons : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public Image soundButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    [Header("All Info Panels")]
    public GameObject allInfoPanels;

    private bool isMuted = false;
    private bool isInfoVisible = true;
    private bool targetFound = false;

    public void OnTargetFound()
    {
        targetFound = true;
        isInfoVisible = true;

        if (allInfoPanels != null)
            allInfoPanels.SetActive(true);
    }

    public void OnTargetLost()
    {
        targetFound = false;

        if (allInfoPanels != null)
            allInfoPanels.SetActive(false);
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;

        if (audioSource != null)
            audioSource.mute = isMuted;

        if (soundButtonImage != null)
            soundButtonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }

    public void ToggleInfo()
    {
        if (allInfoPanels == null)
        {
            Debug.Log("All Info Panels not assigned!");
            return;
        }

        bool newState = !allInfoPanels.activeSelf;
        allInfoPanels.SetActive(newState);

        Debug.Log("Info panels active: " + newState);
    }
}