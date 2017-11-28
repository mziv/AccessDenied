using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : MonoBehaviour {

    public bool cleanPrefOnStart = true;

    public GameObject terminalNotif; //index 0
    public GameObject terminalAlert;

    public GameObject collectableNotif; //index 1
    public GameObject datacardNotif; //index 2

    public GameObject alert; //index 3
    public GameObject smokeNotice;

    private List<GameObject> allNotices = new List<GameObject>();

    void Start()
    {
        if (cleanPrefOnStart) PlayerPrefs.DeleteAll();
        initAllNoticeList();
        close();
    }

    /*
     * UPDATE THIS WHEN ADDING NEW NOTICE
     */
    void initAllNoticeList()
    {
        allNotices.Add(terminalNotif);
        allNotices.Add(terminalAlert);
        allNotices.Add(collectableNotif);
        allNotices.Add(datacardNotif);
        allNotices.Add(alert);
        allNotices.Add(smokeNotice);
    }

    //activate notice group by index
    public void activateNotice(int index)
    {
        switch (index)
        {
            case 0:
                terminalNotification();
                break;
            case 1:
                collectableNotice();
                break;
            case 2:
                dataCardNotice();
                break;
            case 3:
                humanAlert();
                break;
        }
    }

    //0
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

    //3
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

    //1
    public void collectableNotice()
    {
        collectableNotif.SetActive(true);
    }

    //2
    public void dataCardNotice()
    {
        datacardNotif.SetActive(true);
    }

    public void close()
    {
        foreach(GameObject notice in allNotices)
        {
            notice.SetActive(false);
        }
    }
}
