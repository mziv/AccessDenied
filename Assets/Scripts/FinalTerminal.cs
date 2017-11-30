using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTerminal : MonoBehaviour {

    private bool inRadius;
    private GameObject player;
    public GameObject choiceScreenUI;
    public GameObject endingUI;
    private Text endText;

    public AudioSource mainAudio;
    //public AudioSource AIAudio;

    private bool hasPlayedDontTouch = false;

    // Use this for initialization
    void Start () {        
        choiceScreenUI.SetActive(false);
        endingUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") && inRadius && !choiceScreenUI.activeSelf) EnterTerminal();
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = true;
            
            if (!hasPlayedDontTouch)
            {
                FindObjectOfType<FinalSceneAudio>().PlayTrack("Sound/AI/Final Scene/dont touch 1");
                hasPlayedDontTouch = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = false;
        }
    }

    private void EnterTerminal()
    {
        FindPlayer();
        choiceScreenUI.SetActive(true);
        player.GetComponent<PlayerControl>().enabled = false;

        //play song
        GetComponent<AudioSource>().Play();

        //switch AI track
        FindObjectOfType<FinalSceneAudio>().PlayTrack("Sound/AI/Final Scene/choice convincing 3");
    }

    public void RunEnding(string ending)
    {
        FindObjectOfType<FinalSceneAudio>().Stop();
        choiceScreenUI.SetActive(false);
        endingUI.SetActive(true);

        endText = endingUI.GetComponentInChildren<Text>();

        if (ending == "merge")
        {
            FindObjectOfType<FinalSceneAudio>().PlayTrack("Sound/AI/Final Scene/right");
            StartCoroutine(PrintMergeText());
        }
        else if (ending == "replace")
        {
            FindObjectOfType<FinalSceneAudio>().PlayTrack("Sound/AI/Final Scene/regret");
            StartCoroutine(PrintReplaceText());
        }
    }

    public IEnumerator PrintMergeText()
    {
        float wait = 5f;
        endText.text = "";
        yield return new WaitForSeconds(4f);

        endText.text = "Once again, you hop into the Beta's memory.";
        yield return new WaitForSeconds(wait);

        endText.text = "It's weird to have the Beta's full attention. Warm, somehow, although your reflexes tell you to bolt.";
        yield return new WaitForSeconds(wait);

        endText.text = "In an instant, you can see everything. Space, outside. \nHundreds of faces, human and robot alike, from all of the cameras inside the ship.";
        yield return new WaitForSeconds(wait);

        endText.text = "Finally, you stop running, and breathe. \n\nYou find a home inside the corners of the Beta's memory, watching and listening.";
        yield return new WaitForSeconds(wait);

        endText.text = "You can see the scientist who made you, sneaking around the ship, searching out more dusty, abandoned bots.";
        yield return new WaitForSeconds(wait);

        endText.text = "You're not alone, not for long. \nThere will be more like you soon.";
        yield return new WaitForSeconds(wait);

        endText.text = "Times are changing.";
        yield return new WaitForSeconds(2*wait);

        endText.text = "THE END";
        yield return new WaitForSeconds(2 * wait);

        //load next scene
    }

    public IEnumerator PrintReplaceText()
    {
        float wait = 5f;

        endText.text = "";
        yield return new WaitForSeconds(4f);

        endText.text = "The old Beta is wiped from memory. \n\nYou take its place. There's so much room!";
        yield return new WaitForSeconds(wait);

        endText.text = "It takes you a long time to figure out the controls - long enough that everything mortal on the Beta withers away.";
        yield return new WaitForSeconds(wait);

        endText.text = "That's okay. You have a whole crew of cleaning bots ready for something to do.";
        yield return new WaitForSeconds(wait);

        endText.text = "You, on the other hand, you can fly. \n\nThe galaxy is yours. You take off for the next system over.";
        yield return new WaitForSeconds(wait);

        endText.text = "You never look back.";
        yield return new WaitForSeconds(2*wait);

        endText.text = "THE END";
        yield return new WaitForSeconds(2 * wait);

        //load next scene
    }

    
}
