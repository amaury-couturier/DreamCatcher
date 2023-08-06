using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;

    public GameObject winScreen;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;

    private float bestTime = Mathf.Infinity;

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
        UpdateBestTimeText();
    }

    private void UpdateBestTimeText()
    {
        Timer timer = FindObjectOfType<Timer>();
        bestTimeText.text = "Best Time: " + (timer.hasFormat ? bestTime.ToString(timer.timeFormats[timer.format]) : bestTime.ToString());
    }

    public void FinishedGame()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            float finishedTime = timer.currentTime;

            timeText.text = "Your Time: " + (timer.hasFormat ? finishedTime.ToString(timer.timeFormats[timer.format]) : finishedTime.ToString());

            if (finishedTime < bestTime)
            {
                bestTime = finishedTime;
                UpdateBestTimeText();

                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
            }
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
