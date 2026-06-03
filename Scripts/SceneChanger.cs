using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delay = 0.25f; // adjust this (0.3 - 1.0 is usually enough)

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneWithDelay(sceneName));
    }

    IEnumerator ChangeSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}