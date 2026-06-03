using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject cardPanel;
    public GameObject arPanel;
    public GameObject mainCamera;
    public GameObject arCamera;

    public float delay = 0.3f; // adjust if needed

    public void OpenAR()
    {
        StartCoroutine(OpenARWithDelay());
    }

    public void CloseAR()
    {
        StartCoroutine(CloseARWithDelay());
    }

    IEnumerator OpenARWithDelay()
    {
        // Optional: you can play sound here

        yield return new WaitForSeconds(delay);

        cardPanel.SetActive(false);
        arPanel.SetActive(true);

        mainCamera.SetActive(false);
        arCamera.SetActive(true);
    }

    IEnumerator CloseARWithDelay()
    {
        // Optional: play back sound here

        yield return new WaitForSeconds(delay);

        cardPanel.SetActive(true);
        arPanel.SetActive(false);

        mainCamera.SetActive(true);
        arCamera.SetActive(false);
    }
}