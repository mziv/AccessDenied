using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public Canvas map;
    private bool mapOpen = false;

	// Use this for initialization
	void Start () {
        map.enabled = false;	
	}
	
	public void openMap()
    {
        mapOpen = true;
        map.enabled = true;
    }

    public void closeMap()
    {
        mapOpen = false;
        map.enabled = false;
    }

    public bool mapIsOpen()
    {
        return mapOpen;
    }
}
