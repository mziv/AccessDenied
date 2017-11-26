using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour {

    public Animator robotAnimator;
    public Animator scientistAnimator;
    public Animator scientistMovementAnimator;
    public Animator cameraAnimator;

    public Canvas dialogue;
    public GameObject blackout;

    private AudioSource sounds;
    public AudioClip footsteps;
    public AudioClip turningOn;

    //public bool scientistWalking = false;

	// Use this for initialization
	void Start () {
        dialogue.enabled = false;
        blackout.SetActive(false);
        sounds = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ScientistMoving(bool val)
    {
        scientistMovementAnimator.SetBool("move", val);
        //scientistAnimator.SetBool("isWalking", val);
        ScientistWalking(val);
    }

    public void ScientistWalking(bool val)
    {
        scientistAnimator.SetBool("isWalking", val);

        //Play footstep sound if walking.
        print("scientistwalking called" + val);
        if (val) sounds.PlayOneShot(footsteps);
        else sounds.Stop();
    }

    public void RobotOn(bool val)
    {
        robotAnimator.SetBool("robotOn", val);
        if (val) sounds.PlayOneShot(turningOn);
    }

    public void TriggerDialogue(string keyword)
    {
        if(keyword == "there")
        {
            dialogue.GetComponentInChildren<Text>().text = ". . . there.";
        } else if (keyword == "go")
        {
            dialogue.GetComponentInChildren<Text>().text = "I'd better get out of here.";
        } else if (keyword == "just")
        {
            dialogue.GetComponentInChildren<Text>().text = "Just. . . stay safe, okay?";
        } else if (keyword == "sneak")
        {
            dialogue.GetComponentInChildren<Text>().text = "I didn't sneak you halfway across the galaxy to have you mess it up.";
        } else if (keyword == "stay")
        {
            dialogue.GetComponentInChildren<Text>().text = "And stay here!";
        } else if (keyword == "epsilon")
        {
            dialogue.GetComponentInChildren<Text>().text = "We don't want another Epsilon.";
        }
        dialogue.enabled = true;

    }

    public void DialogueOff()
    {
        dialogue.enabled = false;
    }

    public void CameraZoom(bool val)
    {
        cameraAnimator.SetBool("endScene", true);
    }

    public void Blackout()
    {
        blackout.SetActive(true);
    }
}
