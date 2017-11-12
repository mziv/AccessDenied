using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeTrigger : MonoBehaviour {

    public Canvas notice;

    void Start()
    {
        notice.enabled = false;
    }

	void OnTriggerEnter()
    {
        notice.enabled = true;
    }
}
