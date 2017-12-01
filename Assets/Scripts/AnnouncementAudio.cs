using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementAudio : MonoBehaviour {

    private bool hasPlayed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter()
    {
        if (!hasPlayed)
        {
            GetComponent<AudioSource>().Play();
            hasPlayed = true;
        }
        
    }
}
