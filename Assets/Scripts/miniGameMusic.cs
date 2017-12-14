using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGameMusic : MonoBehaviour {

    private AudioSource music;

	// Use this for initialization
	void Start () {
        music = GetComponent<AudioSource>();
	}
	
	public void playAudio()
    {
        music.Play();
    }

    public void stopAudio()
    {
        music.Stop();
    }
}
