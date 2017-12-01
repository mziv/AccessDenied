using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    public Canvas menu;
    public Canvas credits;
    private bool menuIsOpen = false;
    private bool creditsOpen = false;
    private AnnouncementAudio announce;

    private void Start()
    {
        menu.enabled = false;
        credits.enabled = false;
        announce = FindObjectOfType<AnnouncementAudio>();
    }

    // Update is called once per frame
    void Update () {
        openCloseMenuOnESC();
	}

    void openCloseMenuOnESC()
    {
        if (creditsOpen && (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)))
        {
            closeCredits();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && !menuIsOpen)
        {
            enterMenu();
        }else if(Input.GetKeyUp(KeyCode.Escape) && menuIsOpen)
        {
            exitMenu();
        }
    }

    public void closeMenu()
    {
        exitMenu();
    }

    public void playCredits()
    {

    }

    public void quitGame()
    {
        Application.Quit();
    }

    void enterMenu()
    {
        Time.timeScale = 0f; // pauseGame
        announce.pauseAnnounce();
        menuIsOpen = true;
        menu.enabled = true;
    }

    void exitMenu()
    {
        Time.timeScale = 1f;
        announce.resumeAnnounce();
        menuIsOpen = false;
        menu.enabled = false;
    }
  
    public void openCredits()
    {
        credits.enabled = true;
        creditsOpen = true;
    }

    public void closeCredits()
    {
        credits.enabled = false;
        creditsOpen = false;
    }
}
