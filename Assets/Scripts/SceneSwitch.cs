using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Function to switch to the MainMenu scene
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToVictory()
    {
        SceneManager.LoadScene("Scenes/VictoryScene");
    }

    public void SwitchToNextGame()
    {
        SceneManager.LoadScene("Scenes/MainGameScene");
    }


    public void SwitchToLose()
    {
        SceneManager.LoadScene("Scenes/LoseScene");
    }


    


}