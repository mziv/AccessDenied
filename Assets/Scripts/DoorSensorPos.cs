using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensorPos : MonoBehaviour {
    public Vector3 cameraPos;
    private GameObject player;
    public bool checkPlayer = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        LocatePlayer();
        this.transform.position = player.transform.position;
    }

    void LocatePlayer()
    {
        if (checkPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            checkPlayer = false;
        }
    }
}
