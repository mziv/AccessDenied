using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    public Canvas menu;
    private bool menuIsOpen = false;

    private void Start()
    {
        menu.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        openCloseMenuOnESC();
	}

    void openCloseMenuOnESC()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !menuIsOpen)
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
        menuIsOpen = true;
        menu.enabled = true;
    }

    void exitMenu()
    {
        Time.timeScale = 1f;
        menuIsOpen = false;
        menu.enabled = false;
    }
  
}
