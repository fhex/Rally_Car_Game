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

            float minutes = Mathf.FloorToInt(bestTimesaved / 60);
            float seconds = Mathf.FloorToInt(bestTimesaved % 60);
            bestTimetxt.text = ("Best Time ") + string.Format("{0:00}:{1:00}", minutes, seconds);
            //bestTimetxt.SetText("Best Time "+ bestTimesaved.ToString("F1"));
        
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
