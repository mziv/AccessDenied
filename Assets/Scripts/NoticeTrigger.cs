using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeTrigger : MonoBehaviour {

    public GameObject notice;

    void Start()
    {
        notice.SetActive(false);
    }

	void OnTriggerEnter()
    {
        notice.SetActive(true);
    }
}
