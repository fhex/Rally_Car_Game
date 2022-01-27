using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] public float raceTime;
    [SerializeField] public float BestTime;
    [SerializeField] bool timerStarted;
    //public TextMeshPro TimerTxt;
    public TextMeshProUGUI TimerTxt;

    void Start()
    {
        if (TimerTxt == null)
        {
            TimerTxt = GameObject.FindGameObjectWithTag("Timertxt").GetComponent<TextMeshProUGUI>() ;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted && TimerTxt != null)
        {
            raceTime += Time.deltaTime;
            float minutes = Mathf.FloorToInt(raceTime / 60);
            float seconds = Mathf.FloorToInt(raceTime % 60);
            TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            //TimerTxt.SetText(raceTime.ToString("F1"));

        }
        else return;
    }
    public void startTimer()
    {
        timerStarted = true;
    }
    public void SaveRaceTime()
    {
        timerStarted = false;
        if( PlayerPrefs.GetFloat("BestTime") > raceTime || PlayerPrefs.GetFloat("BestTime") == 0)
        { PlayerPrefs.SetFloat("BestTime", raceTime); }
        Debug.Log(raceTime);

    }
}
