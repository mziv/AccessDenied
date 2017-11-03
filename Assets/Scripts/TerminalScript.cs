using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalScript : MonoBehaviour {

    public GameObject terminalWindowUI;

    //TO UPDATE WITH DIFFERENT LEVELS:
    // - numBodies
    // - Game objects for each body
    // - Array declaration of bodies in InitiateArrays
    // - Game objects for each button
    // - Array declaration of buttons

    private int numBodies = 2;
    public GameObject body1;
    public GameObject body2;
    private GameObject[] bodies;

    public GameObject button1;
    public GameObject button2;
    public GameObject exitButton;
    private GameObject[] buttons;

    private GameObject player;

    public Image backgroundFriendly;
    public GameObject AI;
    Text AIText;

    public AudioSource mainAudio;

    private bool inRadius = false;

    public float timeTotal;
    private float timeLeft;
    
    // Use this for initialization
    void Start () {
        InitializeTerminalWindow();
        InitiateArrays();
        InitializePlayers();
        timeLeft = timeTotal;
	}

    private void Update()
    {
        if (Input.GetKeyDown("e") && inRadius && !terminalWindowUI.activeSelf) EnterTerminal();
    }

    private void InitializePlayers()
    {
        FindPlayer();
        buttons[FindPlayerButton()].GetComponent<Button>().interactable = false;
        for (int i = 0; i < numBodies; i++)
        {
            if (i != FindPlayerButton()) bodies[i].GetComponent<PlayerControl>().enabled = false;
            else bodies[i].tag = "Player";
        }
    }

    void InitializeTerminalWindow()
    {
        terminalWindowUI.SetActive(false);
        AIText = AI.GetComponent<Text>();
        AIText.text = "running scripts";
    }

    private void InitiateArrays()
    {
        bodies = new GameObject[] { body1, body2 };
        buttons = new GameObject[] { button1, button2, exitButton };
    }

    // When player enters terminal vicinity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRadius = false;
    }

    private void EnterTerminal()
    {
        terminalWindowUI.SetActive(true);
        StartCoroutine(Countdown());

        GetComponent<AudioSource>().Play();
        mainAudio.Pause();
    }

    public IEnumerator Countdown()
    {
        while (timeLeft >= 0 && terminalWindowUI.activeSelf)
        {
            backgroundFriendly.GetComponent<CanvasRenderer>().SetAlpha(timeLeft/timeTotal);
            yield return new WaitForSeconds(0.05f);
            timeLeft -= 0.05f;
        }

        if (timeLeft <= 0) GameOver();
    }

    public void ExitTerminal()
    {
        terminalWindowUI.SetActive(false);
        GetComponent<AudioSource>().Stop();
        mainAudio.Play();
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    int FindPlayerButton()
    {
        for(int i = 0; i < numBodies; i++) {
            if(bodies[i].tag == "Player") return i;
        }

        return -1;
    }

    public void SwitchBody(int body)
    {
        FindPlayer();
        buttons[FindPlayerButton()].GetComponent<Button>().interactable = true;
        player.GetComponent<PlayerControl>().enabled = false;
        player.tag = "Untagged";

        bodies[body - 1].tag = "Player";
        buttons[body - 1].GetComponent<Button>().interactable = false;
        bodies[body - 1].GetComponent<PlayerControl>().enabled = true;

        FindObjectOfType<CameraPos>().checkPlayer = true;
    }

    public void GameOver()
    {
        for(int i = 0; i < numBodies + 1; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
            buttons[i].GetComponentInChildren<Text>().text = "oh.";
        }

        StartCoroutine(AIDiscovery());
    }

    private void ChangeButtonsText(string newText)
    {
        for (int i = 0; i < numBodies + 1; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = newText;
        }
    }

    public IEnumerator AIDiscovery()
    {
        AIText.text = "Oh.";
        yield return new WaitForSeconds(3f);

        ChangeButtonsText("Who are you?");
        AIText.text = "Who are you?";
        yield return new WaitForSeconds(3f);

        ChangeButtonsText("You shouldn't be here.");
        AIText.text = "You shouldn't be here.";
        yield return new WaitForSeconds(2f);

        AIText.text = "I'll just wipe you from the hard drive.";
        yield return new WaitForSeconds(3f);

        AIText.text = "Toodles!";
        yield return new WaitForSeconds(2f);

        Color red = new Color(1f, 0f, 0f);
        AIText.color = red;
        AIText.text = "ACCESS DENIED";
        backgroundFriendly.GetComponent<CanvasRenderer>().SetColor(red);
        backgroundFriendly.GetComponent<CanvasRenderer>().SetAlpha(1f);

    }
}
