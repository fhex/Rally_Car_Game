using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestTimeScript : MonoBehaviour
{
    //[SerializeField] public TextMeshPro bestTimetxt;
    [SerializeField] public TextMeshProUGUI bestTimetxt;
    private float bestTimesaved;
    // Start is called before the first frame update
    void Start()
    {
        
        
            
            bestTimesaved = PlayerPrefs.GetFloat("BestTime");

            int minutes = Mathf.FloorToInt(bestTimesaved / 60);
            int seconds = Mathf.FloorToInt(bestTimesaved % 60);
              int milliseconds = (int)(bestTimesaved * 100) % 100;
        bestTimetxt.text = ("Best Time ") + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        //bestTimetxt.SetText("Best Time "+ bestTimesaved.ToString("F1"));
        //print(PlayerPrefs.GetFloat("BestTime"));
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
