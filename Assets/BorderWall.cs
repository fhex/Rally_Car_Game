using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWall : MonoBehaviour
{
    GameObject outofBoundTxt;
    [SerializeField] public Vector3 CheckpointLocation;
    [SerializeField] public Quaternion checkpointRot;
    [SerializeField] public GameObject player;
    private void Start()
    {
        outofBoundTxt = GameObject.FindGameObjectWithTag("OutOfBounds");
        outofBoundTxt.SetActive(false);
        CheckpointLocation = new Vector3(0, 0, 0);
        checkpointRot = Quaternion.identity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        HitOutOFBounds(collision);
    }
    private void OnTriggerEnter(Collider other)
    {
        //HitOutOFBounds(other);
    }

    private void HitOutOFBounds(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player")
        {

            if (outofBoundTxt != null)
            {
                outofBoundTxt.SetActive(true);
                if (CheckpointLocation != null)
                {

                    //collision.gameObject.transform.position = CheckpointLocation;
                    StartCoroutine(WaitForSenconds(collision));
                }
            }
        }
        else return;
    }

    
    IEnumerator WaitForSenconds(Collision collision)
    {

        yield return new WaitForSeconds(3);
        TeleportPlayerToCheckPoint();

    }

    private void TeleportPlayerToCheckPoint()
    {
       // player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(CheckpointLocation.x, CheckpointLocation.y + 5, CheckpointLocation.z);
        player.transform.rotation = checkpointRot;
        outofBoundTxt.SetActive(false);
    }
}
