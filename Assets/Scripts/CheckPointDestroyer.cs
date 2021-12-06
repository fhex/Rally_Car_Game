using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointDestroyer : MonoBehaviour
{
    [SerializeField] GameObject CheckPointBurst;
    public bool activated = false;

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
