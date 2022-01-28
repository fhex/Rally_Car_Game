using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravelSoundScript : MonoBehaviour
{
    AudioSource audiosource;
    private float volumeInput;
    private WheelCollider wheelcollider;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        wheelcollider = GetComponentInParent<WheelCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wheelcollider != null && wheelcollider.isGrounded)
        {
            
            volumeInput = Mathf.Abs(FindObjectOfType<CarinputScript>().speed);
            audiosource.pitch = Mathf.Clamp(volumeInput, 15, 100) / 50;
            audiosource.volume = Mathf.Clamp(volumeInput, 25, 60) / 400;
        }
        else
        {
            volumeInput = 0;
            audiosource.pitch = 0;
            audiosource.volume = 0;
        }

    }
}
