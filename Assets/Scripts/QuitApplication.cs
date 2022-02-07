using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    private void Update()
    {
        RespondToQuitButton();
    }
    private void RespondToQuitButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("a");
            Application.Quit();
        }
    }
}
