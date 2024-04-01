using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_menu : MonoBehaviour
{
    public void OnStart_Button ()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OnQuit_Button()
    {
        Application.Quit();
    }

}
