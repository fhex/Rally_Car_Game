using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionEnableRigidBody : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.isKinematic = false;
    }
}
