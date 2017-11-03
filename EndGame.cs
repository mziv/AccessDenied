using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public GameObject text;

	public void OnTriggerEnter()
    {
        text.SetActive(true);
    }
}
