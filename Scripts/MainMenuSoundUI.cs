using UnityEngine;
using UnityEngine.UI;

public class MainMenuSoundUI : MonoBehaviour
{
    public GameObject soundOnButton;
    public GameObject soundOffButton;

    void Start()
    {
        soundOnButton.GetComponent<Button>().onClick.RemoveAllListeners();
        soundOffButton.GetComponent<Button>().onClick.RemoveAllListeners();

        soundOnButton.GetComponent<Button>().onClick.AddListener(TurnMusicOff);
        soundOffButton.GetComponent<Button>().onClick.AddListener(TurnMusicOn);

        UpdateButtons();
    }

    void TurnMusicOff()
    {
        AudioManager.instance.MusicOff();
        UpdateButtons();
    }

    void TurnMusicOn()
    {
        AudioManager.instance.MusicOn();
        UpdateButtons();
    }

    void UpdateButtons()
    {
        soundOnButton.SetActive(!AudioManager.instance.IsMusicMuted);
        soundOffButton.SetActive(AudioManager.instance.IsMusicMuted);
    }
}