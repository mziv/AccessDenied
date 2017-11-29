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
            menuIsOpen = true;
            menu.enabled = true;
        }else if(Input.GetKeyUp(KeyCode.Escape) && menuIsOpen)
        {
            menuIsOpen = false;
            menu.enabled = false;
        }
    }

    public void closeMenu()
    {
        menuIsOpen = false;
        menu.enabled = false;
    }

  
}
