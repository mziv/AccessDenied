using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGGTerminal : MonoBehaviour {

    private GameObject player;
    private bool inRadius = false;
    private CollectionManager inventory;

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
                Debug.Log("opened EGG");
            }
            else
            {
                Debug.Log("Need all three cards");
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
}
