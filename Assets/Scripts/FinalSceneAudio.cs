using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneAudio : MonoBehaviour {

    private AudioSource AIAudio;
    public AudioSource mainAudio;

    private bool hasEntered = false;

	// Use this for initialization
	void Start () {
        AIAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter()
    {
        if (!hasEntered)
        {
            PlayTrack("Sound/AI/Final Scene/AI monologue");
            mainAudio.Stop();
            hasEntered = true;
        }
        
    }

    public void PlayTrack(string trackLocation)
    {
        Stop();
        AIAudio.PlayOneShot(Resources.Load<AudioClip>(trackLocation), 1);
    }

    public void Stop()
    {
        AIAudio.Stop();
    }
}
