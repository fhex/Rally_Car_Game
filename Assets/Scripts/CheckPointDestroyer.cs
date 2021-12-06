using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointDestroyer : MonoBehaviour
{
    [SerializeField] GameObject CheckPointBurst;
    public bool activated = false;
    

    private void Start()
    {
        
        transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position)+2,transform.position.z);
        //gameObject.transform.position.y = Terrain.activeTerrain.SampleHeight(transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CheckPoint");
        //activated = true;
        
        FindObjectOfType<CheckPointHandler>().CheckpointTaken();
        GameObject Firework = Instantiate(CheckPointBurst, transform.position, Quaternion.identity);
        Destroy(Firework, 2);
        Destroy(gameObject);
    }

}
