using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CarModelChanger modelChanger;
    public int chosenCar;
    public static GameManager Instance;
    

    [SerializeField] public bool activateUseJoystick;
    

    // Start is called before the first frame update
    private void Awake()
    {
        Screen.SetResolution(640, 360, true);
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }
    private void Start()
    {
        Debug.Log("GameManagerStarted");
        modelChanger = FindObjectOfType<CarModelChanger>();
        
        //activateUseJoystick = false;
    }

    // Update is called once per frame
    public void toggleJoystick()
    {
        
        activateUseJoystick = !activateUseJoystick;
        //Debug.Log("toggle Joystick Gamemanager");
    }
    public void StartnewGame()
    {
        modelChanger = FindObjectOfType<CarModelChanger>();
        chosenCar = modelChanger.currentCar;
        
        Debug.Log("gamemanager startGame and save current Car" + chosenCar);
        SceneManager.LoadScene(1);
        
    }
    public void FreeRoamLevel()
    {
        modelChanger = FindObjectOfType<CarModelChanger>();
        chosenCar = modelChanger.currentCar;       
        SceneManager.LoadScene(2);
    }
    

    public void RestartLevel()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
}
