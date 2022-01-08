using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButtonScript : MonoBehaviour
{
    MusicPlayerScript musicPlayerScript;

    private void Start()
    {
        if (musicPlayerScript == null)
        {
            musicPlayerScript = FindObjectOfType<MusicPlayerScript>();
        }
    }

    public void toggleMusic()
    {
        if (musicPlayerScript == null) return;
        else
        FindObjectOfType<MusicPlayerScript>().toggleMusic();
    }

}
