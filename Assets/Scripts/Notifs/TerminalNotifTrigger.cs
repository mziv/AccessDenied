using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalNotifTrigger : MonoBehaviour {

    public GameObject notice;

    void Start()
    {
        notice.SetActive(false);
    }

    void OnTriggerEnter()
    {
        notice.SetActive(true);
        FindObjectOfType<NoticeManager>().terminalNotification();
    }
}
