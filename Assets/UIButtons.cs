using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{

    //Methods for game restart button and quit button

    public void OnRestartClicked()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
