using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CarModelChanger modelChanger;
    public int chosenCar;
    [SerializeField] public bool UseJoystick;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
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

        SceneManager.LoadScene(1);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
