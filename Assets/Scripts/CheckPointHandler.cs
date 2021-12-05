using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPointHandler : MonoBehaviour
{
    public static GameObject[] CheckPointsList;
    public int numberOfCheckPoints;
    private int CurrentCheckPointInt = 0;
    private Vector3 currentCheckPointPos;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject checkPointArrow;

    void Start()
    {
        
        CheckPointStartingMethod();
        winScreen = GameObject.FindGameObjectWithTag("WinScreen");
        if (winScreen == null) return;
        else
        {
            winScreen.SetActive(false);
        }
    }
    public void CheckPointStartingMethod()
    {
        Debug.Log("checkPointHandler Start");
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint").OrderBy(go => go.name).ToArray();   //Make a list of checkpoints in Scene in name order
        Debug.Log(CheckPointsList);
        if (checkPointArrow == null)//Check if there is a Checkpoint arrow make it visible.
        {
            checkPointArrow = GameObject.FindGameObjectWithTag("Arrow");

            if (checkPointArrow == null)
            { Debug.Log("no CheckpointArrow"); return; }
            else
                checkPointArrow.SetActive(true);
        }
        if (CheckPointsList.Length <= 0)
        {
            checkPointArrow.SetActive(false);
            return;
        }
        else
        {
            numberOfCheckPoints = CheckPointsList.Length;
            if (numberOfCheckPoints < 1) { checkPointArrow.SetActive(false); } //If there are no more Checkpoint turn of Arrow
            foreach (GameObject cp in CheckPointsList)
            {
                cp.gameObject.SetActive(false);

            }

            CheckPointsList[CurrentCheckPointInt].SetActive(true); //set the First Checkpoint Active.
            UpdateCheckPointPos();//check position of Checkpoint for the Arrow.
        }
        
    }

    private void FixedUpdate()
    {
        
        if (currentCheckPointPos == null || checkPointArrow == null) return; //Check that there are checkpoints and Arrow before turning the Arrow.
                   
        checkPointArrow.transform.LookAt(currentCheckPointPos);
    }
    private void UpdateCheckPointPos()
    {
        currentCheckPointPos = FindObjectOfType<CheckPointDestroyer>().transform.position; //called to Locate the position of active Checkpoint 1 att the time.
    }
    public void CheckpointTaken() //When you trigger checkpoint this is called.
    {
        
        Debug.Log(numberOfCheckPoints);
        CheckPointsList[CurrentCheckPointInt].SetActive(false);  
        if (CurrentCheckPointInt >= numberOfCheckPoints -1)
        {
            Debug.Log("All checkpoints");
            
            StartCoroutine("AllCheckPoints");
            if (checkPointArrow != null)
            {
                checkPointArrow.SetActive(false);
            }
            return;
        }
        else if (CurrentCheckPointInt < numberOfCheckPoints - 1)
        {
            
            CurrentCheckPointInt++;
            CheckPointsList[CurrentCheckPointInt].SetActive(true);
            UpdateCheckPointPos();
            Debug.Log(CurrentCheckPointInt);
        }
        
    }
    private IEnumerator AllCheckPoints()
    {
        winScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        FindObjectOfType<GameManager>().MainMenu();
    }
}
