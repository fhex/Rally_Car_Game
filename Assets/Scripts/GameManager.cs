using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CarModelChanger modelChanger;
    public int chosenCar;
    public static GameManager Instance;
    [SerializeField] public bool UseJoystick;
    // Start is called before the first frame update
    private void Awake()
    {
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
        modelChanger = FindObjectOfType<CarModelChanger>();
        UseJoystick = false;
    }

    // Update is called once per frame
    public void toggleJoystick()
    {
        UseJoystick = !UseJoystick;
    }
    public void StartnewGame()
    {
        chosenCar = modelChanger.currentCar;
        Debug.Log("gamemanger save current Car" + chosenCar);
        SceneManager.LoadScene(1);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
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
