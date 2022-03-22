using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Track1;
    public AudioSource Track2;
    public AudioSource Track3;

    public int TrackSelector;
    public int TrackHistory;

    
    // Start is called before the first frame update
    void Start()
    {
        TrackSelector = Random.Range(0,3);
        if(TrackSelector == 0)
        {
            Track1.Play();
            TrackHistory = 1;
        }
        if(TrackSelector == 1)
        {
            Track2.Play();
            TrackHistory = 2;
        }
        if(TrackSelector == 2)
        {
            Track3.Play();
            TrackHistory = 3;
        }
    }

    public void MuteD(bool state)
    {
        if(state)
    {
    Track1.mute= true;
    Track2.mute= true;
    Track3.mute= true;
    }
    else
    {
        Track1.mute= false;
        Track2.mute= false;
        Track3.mute= false;
    }

    }


    // Update is called once per frame
    void Update()
    {
        if(Track1.isPlaying == false && Track2.isPlaying == false && Track3.isPlaying == false)
        {
            TrackSelector = Random.Range(0,3);
                if(TrackSelector == 0)
        {
            Track1.Play();
            TrackHistory = 1;
        }
        if(TrackSelector == 1)
        {
            Track2.Play();
            TrackHistory = 2;
        }
        if(TrackSelector == 2)
        {
            Track3.Play();
            TrackHistory = 3;
        }
        }
    }
}
