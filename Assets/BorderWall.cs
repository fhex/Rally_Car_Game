using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWall : MonoBehaviour
{
    GameObject outofBoundTxt;

    private void Start()
    {
        outofBoundTxt = GameObject.FindGameObjectWithTag("OutOfBounds");
        outofBoundTxt.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (outofBoundTxt != null)
        {
            outofBoundTxt.SetActive(true);
            StartCoroutine(WaitForSenconds());
        }
        else return;
    }
    IEnumerator WaitForSenconds()
    {
        yield return new WaitForSeconds(3);
        outofBoundTxt.SetActive(false);

    }
}
