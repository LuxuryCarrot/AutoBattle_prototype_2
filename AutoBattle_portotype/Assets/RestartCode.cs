using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCode : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("BattleScenes");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
