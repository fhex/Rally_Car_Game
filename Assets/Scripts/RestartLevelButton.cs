using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartLevelButton : MonoBehaviour
{
    

    private void Start()
    {
        
    }
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
    public void toggleJoystick()
    {
        
        FindObjectOfType<GameManager>().toggleJoystick(); 
    }
}
