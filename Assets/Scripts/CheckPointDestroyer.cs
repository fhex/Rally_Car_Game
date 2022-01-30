using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointDestroyer : MonoBehaviour
{
    [SerializeField] GameObject CheckPointBurst;
    public bool activated = false;
    [SerializeField] private GameObject player;
    [SerializeField] public Vector3 checkpointLocation;
    [SerializeField] public Quaternion checkpointRot;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position)+2,transform.position.z);
        //gameObject.transform.position.y = Terrain.activeTerrain.SampleHeight(transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("CheckPoint");
            checkpointLocation = new Vector3(other.transform.parent.position.x, other.transform.parent.position.y, other.transform.parent.position.z); //Get PlayerPos when getting Checkpoint
            checkpointRot = player.transform.rotation; //get player Rot in checkpoint
            FindObjectOfType<BorderWall>().CheckpointLocation = checkpointLocation; //Save pos and rot in Borderwall Script
            FindObjectOfType<BorderWall>().checkpointRot = checkpointRot;
            FindObjectOfType<CheckPointHandler>().CheckpointTaken(); //mark checkpoint as taken in handler
            GameObject Firework = Instantiate(CheckPointBurst, transform.position, Quaternion.identity); //Spawn firework
            Destroy(Firework, 2); //Destroy checkpoint and fireworks
            Destroy(gameObject);
        }
    }

}
