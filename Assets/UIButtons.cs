using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void OnRestartClicked()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
