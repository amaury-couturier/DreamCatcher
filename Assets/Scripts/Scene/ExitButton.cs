using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGameOnClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
