using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointDestroyer : MonoBehaviour
{
    [SerializeField] GameObject CheckPointBurst;
    public bool activated = false;
    [SerializeField] private GameObject player;
    [SerializeField] public Vector3 CheckpointLocation;
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
            CheckpointLocation = new Vector3(other.transform.parent.position.x, other.transform.parent.position.y, other.transform.parent.position.z);
            //checkpointRot = Quaternion.Euler(other.transform.rotation.x, other.transform.rotation.y, other.transform.rotation.z);
            checkpointRot = player.transform.rotation;
            FindObjectOfType<BorderWall>().CheckpointLocation = CheckpointLocation;
            FindObjectOfType<BorderWall>().checkpointRot = checkpointRot;
            FindObjectOfType<CheckPointHandler>().CheckpointTaken();
            GameObject Firework = Instantiate(CheckPointBurst, transform.position, Quaternion.identity);
            Destroy(Firework, 2);
            Destroy(gameObject);
        }
    }

}
