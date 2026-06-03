using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class QuizManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject[] questionPanels;
    public GameObject resultPanel;

    [Header("Score")]
    public TextMeshProUGUI scoreText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    [Header("Timer")]
    public Slider timerSlider;
    public Image fillImage;
    public float timePerQuestion = 6f;

    private int currentQuestion = 0;
    private int score = 0;

    private float currentTime;
    private bool isAnswering = false;

    void Start()
    {
        resultPanel.SetActive(false);
        timerSlider.gameObject.SetActive(true);
        ShowQuestion(0);
    }

    void Update()
    {
        if (!isAnswering) return;

        currentTime -= Time.deltaTime;
        timerSlider.value = currentTime;

        float t = currentTime / timePerQuestion;
        fillImage.color = Color.Lerp(Color.red, Color.green, t);

        if (currentTime <= 0)
        {
            TimeUp();
        }
    }

    void ShowQuestion(int index)
    {
        foreach (GameObject panel in questionPanels)
        {
            panel.SetActive(false);
        }

        questionPanels[index].SetActive(true);

        EnableButtons();
        StartTimer();
    }

    void StartTimer()
    {
        currentTime = timePerQuestion;
        timerSlider.maxValue = timePerQuestion;
        timerSlider.value = currentTime;

        isAnswering = true;
    }

    public void Answer(int correctAnswerIndex)
    {
        if (!isAnswering) return;

        isAnswering = false;

        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        DisableButtons();

        if (correctAnswerIndex == 1)
        {
            score++;
            audioSource.PlayOneShot(correctSound);
            StartCoroutine(CorrectAnimation(clickedButton));
        }
        else
        {
            audioSource.PlayOneShot(wrongSound);
            StartCoroutine(WrongAnimation(clickedButton));
        }

        StartCoroutine(NextQuestionDelay());
    }

    void TimeUp()
    {
        isAnswering = false;

        DisableButtons();

        StartCoroutine(NextQuestionDelay());
    }

    IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(0.6f);

        currentQuestion++;

        if (currentQuestion < questionPanels.Length)
        {
            ShowQuestion(currentQuestion);
        }
        else
        {
            ShowResult();
        }
    }

    void ShowResult()
    {
        isAnswering = false;

        timerSlider.gameObject.SetActive(false);

        foreach (GameObject panel in questionPanels)
        {
            panel.SetActive(false);
        }

        resultPanel.SetActive(true);

        scoreText.text = score + " / " + questionPanels.Length;

        if (score == questionPanels.Length)
        {
            scoreText.text += "\nPerfect! 🎉";
        }
    }

    // 🎯 SMOOTH CORRECT ANIMATION
    IEnumerator CorrectAnimation(Button btn)
    {
        Vector3 startPos = btn.transform.localPosition;
        Vector3 targetPos = startPos + new Vector3(0, 25, 0);

        float duration = 0.25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t); // smooth step

            btn.transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);

            btn.transform.localPosition = Vector3.Lerp(targetPos, startPos, t);
            yield return null;
        }

        btn.transform.localPosition = startPos;
    }

    // ❌ SMOOTH WRONG ANIMATION
    IEnumerator WrongAnimation(Button btn)
    {
        Vector3 startPos = btn.transform.localPosition;

        float duration = 0.3f;
        float elapsed = 0f;

        float strength = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float damping = 1f - (elapsed / duration);
            float offsetX = Mathf.Sin(elapsed * 80f) * strength * damping;

            btn.transform.localPosition = startPos + new Vector3(offsetX, 0, 0);

            yield return null;
        }

        btn.transform.localPosition = startPos;
    }

    void DisableButtons()
    {
        Button[] buttons = questionPanels[currentQuestion].GetComponentsInChildren<Button>();
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
    }

    void EnableButtons()
    {
        Button[] buttons = questionPanels[currentQuestion].GetComponentsInChildren<Button>();
        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }
    }

    public void RestartQuiz()
    {
        currentQuestion = 0;
        score = 0;

        resultPanel.SetActive(false);

        timerSlider.gameObject.SetActive(true);

        ShowQuestion(0);
    }
}