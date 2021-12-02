using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collissionCarEffects : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float hitMagnitude;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > hitMagnitude)
        {
            audioSource.Play();
        }
    }
}
