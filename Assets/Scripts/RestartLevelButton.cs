using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelButton : MonoBehaviour
{
    
    public void RestartLevel()
    {
        FindObjectOfType<GameManager>().RestartLevel();
        
    }
    public void MainMenu()
    {
        FindObjectOfType<GameManager>().MainMenu();
    }
    public void startNewGame()
    {
        FindObjectOfType<GameManager>().StartnewGame();
    }
}
