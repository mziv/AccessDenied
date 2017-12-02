using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorTrigger : MonoBehaviour {

    private bool inRange = false;
    private bool projectorOn = false;
    public GameObject particle;

    private void Start()
    {
        particle.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyUp("e") && inRange && !projectorOn)
        {
            particle.SetActive(true);
            projectorOn = true;
        }
        else if(Input.GetKeyUp("e") && inRange && projectorOn)
        {
            particle.SetActive(false);
            projectorOn = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
