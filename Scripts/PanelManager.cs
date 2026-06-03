using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject settingsPanel; // Assign in inspector

    void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false); // hide at start
    }

    // OPEN settings
    public void ShowSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    // CLOSE settings
    public void CloseSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}