using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviourr : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Cameratest");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
