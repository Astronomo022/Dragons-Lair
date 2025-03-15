using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Shows in inspector 
    [Header("Scene Management")]
    public string newGameScene; // We can change the scene we want to use in inspector
    //write more as it comes
    public void NewGameLoad()
    {
        SceneManager.LoadScene(newGameScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
