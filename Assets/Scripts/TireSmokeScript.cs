using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireSmokeScript : MonoBehaviour
{
    ParticleSystem tireSmoke;
    private float speed;
    
    public float hSliderValue = 5.0f;
    private WheelCollider wheelcollider;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        tireSmoke = GetComponent<ParticleSystem>();
        wheelcollider = GetComponentInParent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        speed = Mathf.Abs( FindObjectOfType<CarinputScript>().speed);
        if (wheelcollider !=null && wheelcollider.isGrounded)
        {
            Debug.Log(wheelcollider);
            var emission = tireSmoke.emission;
            emission.rateOverTime = speed;
        }
        else
        {
            Debug.Log(wheelcollider);
            var emission = tireSmoke.emission;
            emission.rateOverTime = 2;
        }
    }
}
