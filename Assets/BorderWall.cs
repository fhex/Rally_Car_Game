using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWall : MonoBehaviour
{
    GameObject outofBoundTxt;
    [SerializeField] public Vector3 CheckpointLocation; //Location of last Checkpoint
    [SerializeField] public Quaternion checkpointRot; //Player Rotation in last Checkpoint
    [SerializeField] public GameObject player;
    private void Start()
    {
        outofBoundTxt = GameObject.FindGameObjectWithTag("OutOfBounds");
        outofBoundTxt.SetActive(false);
        CheckpointLocation = new Vector3(0, 0, 0); //StartPoint
        checkpointRot = Quaternion.identity;//Start Rotation
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && player != null)
        {
            TeleportPlayerToCheckPoint();
        }
        else return;

    }
    private void OnTriggerEnter(Collider other) //Trigger enter activates Out of Bounds
    {
        HitOutOFBounds(other);
    }

    private void HitOutOFBounds(Collider other)
    {
        
        if (other.gameObject.tag == "Player") //Check that it's the player triggering
        {

            if (outofBoundTxt != null) //Check that there is a out of bounds info text in Canvas
            {
                outofBoundTxt.SetActive(true); //set out of bounds text active
                if (CheckpointLocation != null) //Check that there are Checkpoint
                {

                    //collision.gameObject.transform.position = CheckpointLocation;
                    StartCoroutine(WaitForSenconds(other)); 
                }
            }
        }
        else return;
    }

    
    IEnumerator WaitForSenconds(Collider other) //Slow down time and wait for some seconds before Teleport. Timescale set in GameManagerStart
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSeconds(2);
        TeleportPlayerToCheckPoint();

    }

    private void TeleportPlayerToCheckPoint() //Teleport player to Pos/Ros of last Checkpoint
    {
        // player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //Stop Movement
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.transform.position = new Vector3(CheckpointLocation.x, CheckpointLocation.y + 3, CheckpointLocation.z); //teleport Location
        player.transform.rotation = checkpointRot; //rotate 

        outofBoundTxt.SetActive(false); //turn of out of bounds text
        Time.timeScale = 1; //turn of slowmotion
        Time.fixedDeltaTime = 0.02F;
    }
}
