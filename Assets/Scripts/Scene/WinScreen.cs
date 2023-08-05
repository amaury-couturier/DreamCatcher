using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Text finalTimeText;
    public Timer timer;

    private void Start()
    {
        finalTimeText.gameObject.SetActive(false); 
    }

    public void DisplayFinalTime()
    {
        finalTimeText.text = "Final Time: " + timer.timerText.text;
        finalTimeText.gameObject.SetActive(true);
    }
}
