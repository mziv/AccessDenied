using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeTrigger : MonoBehaviour {

    public string noticeName;
    public GameObject notice;
    private static string[] validNoticeNames = { "collectable", "dataCard", "terminal", "humanSmoke" };
    private NoticeManager manager;

    void Start()
    {
        //Error checking that input name is 
        bool containsName = false;
        foreach(string notice in validNoticeNames)
        {
            if (notice == noticeName) containsName = true;
        }
        if (!containsName) Debug.LogError("Input name for notice trigger must be valid.");
        manager = FindObjectOfType<NoticeManager>();
    }

    //terminal 0
    //collectable 1
    //datacard 2
    //humanSmoke 3
    private static int TERMINAL =       0;
    private static int COLLECTABLE =    1;
    private static int DATACARD =       2;
    private static int HUMANSMOKE =     3;

    int getIndexFromNoticeName()
    {
        int index = -1;
        switch (noticeName)
        {
            case "collectable":
                index = COLLECTABLE;
                break;
            case "dataCard":
                index = DATACARD;
                break;
            case "terminal":
                index = TERMINAL;
                break;
            case "humanSmoke":
                index = HUMANSMOKE;
                break;
        }
        return index;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int index = getIndexFromNoticeName();
            bool shown = false;
            //check to see is the notice is already displayed
            if(PlayerPrefs.GetInt(index.ToString()) == 1)
            {
                shown = true;
            }
            if(!shown)
            {
                manager.activateNotice(index);
                //document the notice as shown
                PlayerPrefs.SetInt(index.ToString(), 1);
            }
        }
    }
}
