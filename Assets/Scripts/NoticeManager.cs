using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : MonoBehaviour {

    public GameObject terminalNotif;
    public GameObject terminalAlert;
    public GameObject collectableNotif;
    public GameObject datacardNotif;
    public GameObject alert;
    public GameObject smokeNotice;

    void Start()
    {
        
    }

    public void terminalNotification()
    {
        terminalNotif.SetActive(true);
        terminalAlert.SetActive(false);
    }

    public void nextTerminalNotif()
    {
        terminalNotif.SetActive(false);
        terminalAlert.SetActive(true);
    }

    public void humanAlert()
    {
        alert.SetActive(true);
        smokeNotice.SetActive(false);
    }

    public void nextNotice()
    {
        alert.SetActive(false);
        smokeNotice.SetActive(true);
    }

    public void close()
    {
        terminalAlert.SetActive(false);
        terminalNotif.SetActive(false);
        smokeNotice.SetActive(false);
        collectableNotif.SetActive(false);
        datacardNotif.SetActive(false);
    }
}
