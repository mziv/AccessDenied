using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGGTerminal : MonoBehaviour {

    private GameObject player;
    private bool inRadius = false;
    private CollectionManager inventory;
    public GameObject needCards;
    public GameObject easterEgg;
    public bool checkPlayer = false;

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = FindObjectOfType<CollectionManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") && inRadius && player.tag == "Player")
        {
            if (hasPermission())
            {
                LoadEasterEgg();
            }
            else
            {
                LoadNeedCards();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRadius = false;
        }
    }

    private bool hasPermission()
    {
        return (inventory.card1 && inventory.card2 && inventory.card3);
    }

    public void LoadEasterEgg()
    {
        easterEgg.SetActive(true);
        page1.SetActive(true);
    }

    public void LoadNextPage()
    {
        if (page1.activeSelf)
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
        else if (page2.activeSelf)
        {
            page2.SetActive(false);
            page3.SetActive(true);
        }
    }

    public void LoadNeedCards()
    {
        needCards.SetActive(true);
    }

    public void AllOff()
    {
        needCards.SetActive(false);
        easterEgg.SetActive(false);
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
    }

    void updatePlayer()
    {
        if (checkPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            checkPlayer = false;
        }
    }
}
