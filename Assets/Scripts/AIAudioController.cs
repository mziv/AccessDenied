using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAudioController : MonoBehaviour {

    public AudioSource AIDetectionAudio;
    public AudioSource AIRoutineAudio;
    private AudioClip[] routineClips;
    private AudioClip[] detectionClips;
    private AudioClip accessDenied;

    // Use this for initialization
    void Start () {
        routineClips = Resources.LoadAll<AudioClip>("Sound/AI/Routine");
        detectionClips = Resources.LoadAll<AudioClip>("Sound/AI/Detection");
        accessDenied = Resources.Load<AudioClip>("Sound/AI/access denied final");
    }
	
	// Update is called once per frame
	void Update () {
        if (!AIDetectionAudio.isPlaying)
        {
            FindObjectOfType<Minigame>().playingSound = false;
            PlayRoutineAudio();
        }


    }

    public void PlayDetectionSound()
    {
        //AIAudio.clip = ;
        if (!FindObjectOfType<Minigame>().playingSound)
        {
            FindObjectOfType<Minigame>().playingSound = true;
            //AIRoutineAudio.Pause();
            AIRoutineAudio.volume = 0;
            AIDetectionAudio.PlayOneShot(detectionClips[Random.Range(0, detectionClips.Length)], 4);
        }
        
    }

    public void PlayRoutineAudio()
    {
        //AIRoutineAudio.Play();
        AIRoutineAudio.volume = 1;
        if (!AIRoutineAudio.isPlaying)
        {
            AIRoutineAudio.Stop();
            AIRoutineAudio.PlayOneShot(routineClips[Random.Range(0, routineClips.Length)], 4);
        }
        
    }

    public void PlayAccessDenied()
    {
        if (!FindObjectOfType<Minigame>().playingSound)
        {
            FindObjectOfType<Minigame>().playingSound = true;
            //AIRoutineAudio.Pause();
            AIRoutineAudio.volume = 0;
            AIDetectionAudio.PlayOneShot(accessDenied, 4);
        }
    }
}
