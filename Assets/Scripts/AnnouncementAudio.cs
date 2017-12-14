using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementAudio : MonoBehaviour {

    private bool hasPlayed = false;
    private AudioSource announcement;
    private bool paused = false;

	// Use this for initialization
	void Start () {
        announcement = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter()
    {
        if (!hasPlayed && PlayerPrefs.GetInt("announce") != 1)
        {
            announcement.Play();
            PlayerPrefs.SetInt("announce", 1);
            hasPlayed = true;
        }
        
    }

    public void pauseAnnounce()
    {
        if (announcement.isPlaying && !paused)
        {
            announcement.Pause();
            paused = true;
        }
    }

    public void resumeAnnounce()
    {
        if (paused)
        {
            announcement.UnPause();
            paused = false;
        }
    }
}
