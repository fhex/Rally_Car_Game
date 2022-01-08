using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    public static MusicPlayerScript instanceRef;
    [SerializeField] AudioSource musicTrack;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
            Destroy(gameObject);

    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (musicTrack == null)
        {
            musicTrack = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleMusic()
    {
        if (musicTrack == null) return;
        if (musicTrack.isPlaying)
        {
            musicTrack.Pause();
        }
        else musicTrack.Play();
    }
}
