using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneAudio : MonoBehaviour {

    private AudioSource AIAudio;
    public AudioSource AIMonologue;
    public AudioSource mainAudio;

    private bool hasEntered = false;
    private bool unpauseWhenDone = false;

	// Use this for initialization
	void Start () {
        AIAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (unpauseWhenDone)
        {
            if (!AIAudio.isPlaying)
            {
                AIMonologue.UnPause();
                unpauseWhenDone = false;
            }
        }
	}

    private void OnTriggerEnter()
    {
        if (!hasEntered)
        {
            //PlayTrack("Sound/AI/Final Scene/AI monologue");
            AIMonologue.PlayOneShot(Resources.Load<AudioClip>("Sound/AI/Final Scene/AI monologue"), .75f);
            mainAudio.Stop();
            hasEntered = true;
        }

    }

    public void PlayTrack(string trackLocation)
    {
        AIMonologue.Pause();
        Stop();
        AIAudio.PlayOneShot(Resources.Load<AudioClip>(trackLocation), .8f);
    }

    public void Stop()
    {
        AIAudio.Stop();
    }

    public void UnpauseMonologue()
    {
        unpauseWhenDone = true;
        
    }
}
